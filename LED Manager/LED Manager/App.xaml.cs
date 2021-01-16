using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

using LEDLib;
using Microsoft.Win32;

namespace Wpf
{
	public partial class App : System.Windows.Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			this._components = new Container();
			this._notifyIcon = new NotifyIcon(this._components)
			{
				ContextMenuStrip = new ContextMenuStrip(),
				Icon = Wpf.Properties.Resources.tray,
				Text = "Tray LED Manager",
				Visible = true,
			};


			// !!!! IMPORTANT activate new instance of library sub-classes
			this._ledManager = new LedManager();
			this._utility = new Utility();
			this._osd = new OsdForm();

			
			//Check if silent, autostart and KB LED manage/enabled in saved settings
			silentSwitch = _utility.GetSilent();
			autostartSwitch = _utility.GetAutostart();
			manageKBswitch = _utility.GetManageKB();
			
			//to be done - get model
			ClevoModel = _utility.GetModel();
			if(ClevoModel ==0){
				if(System.Windows.MessageBox.Show("Enable cover LED management for Clevo P870XX series? (may cause WMI freeze issues with other models like P775XX)","Enable Cover LED", MessageBoxButton.YesNo) == MessageBoxResult.Yes){
						_utility.SetModel(1);
						ClevoModel = 1;
				}
				else {
						_utility.SetModel(2);
						ClevoModel = 2;
				}
			}
			
			//setup menu items
			this.AddMenuItems(this._notifyIcon.ContextMenuStrip.Items);

			
			//setup events	
			this._notifyIcon.ContextMenuStrip.Opening += this.OnContextMenuStrip_Opening;	
			this._notifyIcon.DoubleClick += this.setKB_Click;			
			this._ledManager.OnLogoSet += delegate(int f, string s)
			{
					if (!silentSwitch) _osd.ShowLogoOsd(f);
			};
						
			//after start - set last saved settings for kb/logo
			if (ClevoModel==1) this.RestoreLastLogo();
			//if kbmanagement was enabled - restore last kb saved settings
			if(manageKBswitch)	{
				RestoreLastKBsettings();
				RegisterHotkeys(true);
			}
			//subscribe to sleep mode to restore settings after sleep
			SystemEvents.PowerModeChanged += OnPowerChange;

		}
		

		/// <summary>
		/// MAIN PROCEDURES
		/// </summary>
			
		//manual setup of menu items
		private void AddMenuItems(ToolStripItemCollection items)
		{
			
			if (ClevoModel ==1){
				
			this._currentLogoLabel = new ToolStripLabel("???");
			items.Add(this._currentLogoLabel);
			
			items.Add(new ToolStripSeparator());	
			
			this._menuBlack = new ToolStripMenuItem ("Set BLACK (Disable)", null, new EventHandler(this.setLogo_ClickByColor));
			this._menuWhite = new ToolStripMenuItem ("White", null, new EventHandler(this.setLogo_ClickByColor));
			this._menuOrange = new ToolStripMenuItem ("Orange", null, new EventHandler(this.setLogo_ClickByColor));
			this._menuBlue = new ToolStripMenuItem ("Blue", null, new EventHandler(this.setLogo_ClickByColor));
			this._menuWhiteBlue = new ToolStripMenuItem ("White Blue", null, new EventHandler(this.setLogo_ClickByColor));
			this._menuGreen = new ToolStripMenuItem ("Green", null, new EventHandler(this.setLogo_ClickByColor));	
			this._menuYellow = new ToolStripMenuItem ("Yellow", null, new EventHandler(this.setLogo_ClickByColor));	
			this._menuRed = new ToolStripMenuItem ("Red", null, new EventHandler(this.setLogo_ClickByColor));	
			this._menuPurple = new ToolStripMenuItem ("Purple", null, new EventHandler(this.setLogo_ClickByColor));
			
			_menuBlack.Tag = 0;
			_menuWhite.Tag = 1;
			_menuOrange.Tag = 2;
			_menuBlue.Tag = 3;
			_menuWhiteBlue.Tag = 4;
			_menuGreen.Tag = 5;
			_menuYellow.Tag = 6;
			_menuRed.Tag = 7;
			_menuPurple.Tag = 8;
			
			items.Add(_menuBlack);
			items.Add(_menuWhite);
			items.Add(_menuOrange);
			items.Add(_menuBlue);
			items.Add(_menuWhiteBlue);
			items.Add(_menuGreen);
			items.Add(_menuYellow);
			items.Add(_menuRed);
			items.Add(_menuPurple);
		

			}
			else{
				this._currentLogoLabel = new ToolStripLabel("Other Clevo Model");
				items.Add(this._currentLogoLabel);
			}
			
			items.Add(new ToolStripSeparator());
			
			items.Add(new ToolStripMenuItem("Keyboard LED setup", null, new EventHandler(this.setKB_Click)));
			
			items.Add(new ToolStripSeparator());
			

			
			this._menuManageKB = new ToolStripMenuItem ("Enable KB LED management", null, new EventHandler(this.EnableKBManage_Click));
			if (ClevoModel !=1){
				manageKBswitch=true;
				_menuManageKB.Checked=true;
				items.Add(_menuManageKB);
				_utility.SetManageKB(true);
			}
			else{
				if (manageKBswitch) _menuManageKB.Checked=true;	
				items.Add(_menuManageKB);
			}

			
			this._menuSilent = new ToolStripMenuItem ("Silent / no OSD", null, new EventHandler(this.setSilent));
			if (silentSwitch) _menuSilent.Checked=true;
			items.Add(_menuSilent);

			
			this._menuAutostart = new ToolStripMenuItem ("Start with Windows", null, new EventHandler(this.setAutostart));
			if (autostartSwitch) _menuAutostart.Checked=true;
			items.Add(_menuAutostart);


			items.Add(new ToolStripSeparator());
			
			items.Add(new ToolStripMenuItem("Reset all settings", null, new EventHandler(this.onReset_Clicked)));
			
			items.Add(new ToolStripSeparator());
			ToolStripLabel _madeby = new ToolStripLabel("@rzrwolf for NBreview/4PDA");
			_madeby.ForeColor = System.Drawing.Color.SlateGray;
			items.Add(_madeby);

			items.Add(new ToolStripSeparator());
			items.Add(new ToolStripMenuItem("Exit", null, new EventHandler(this.onExit_Clicked)));
		}
		
		
		/// <summary>
		/// SOME GENERAL PROCEDURES
		/// </summary>


		private void RegisterHotkeys (bool setstate){
			if (setstate) {
				_hotKeySubsract = new HotKey(Key.Subtract, KeyModifier.Ctrl, OnHKLedBrightnessDOWN);
				_hotKeyAdd = new HotKey(Key.Add, KeyModifier.Ctrl, OnHKLedBrightnessUP); 
				_hotKeyMultiply = new HotKey(Key.Multiply, KeyModifier.Ctrl, OnHKLedOnOff); 
				_hotKeyDivide = new HotKey(Key.Divide, KeyModifier.Ctrl, OnHKLedTimeout); 
				_hotKey0 = new HotKey(Key.NumPad0, KeyModifier.Ctrl, OnHKCoverOff); 
								_hotKeyDot = new HotKey(Key.Decimal, KeyModifier.Ctrl, OnHKCoverCycle); 
			}
			else if(!setstate){
				_hotKeySubsract.Dispose();
				_hotKeyAdd.Dispose();
				_hotKeyMultiply.Dispose();
				_hotKeyDivide.Dispose();
			}

		}
		
	
		//read and set last logo logic
		private void RestoreLastLogo()
		{

			//check if RGB logo was saved last and set RGB logo
			if (this._utility.GetLastLogoRGBonORoff) 
			{
				int[] rgbarray =_utility.GetLastRGBlogo;
				_ledManager.KBSetColorByPart(rgbarray[0],rgbarray[1],rgbarray[2], "cover");
			}
			//if no RGB - setup last saved preset color
			else {
				_ledManager.SetLogoWmi(this._utility.GetLastLogo);
			}
		}
		
		
		//restore kb state
		private void RestoreLastKBsettings()
		{
			//check if LED KB enabled in settings
			kbLEDSwitch = _utility.GetLastKBLEDstatus();
			//if yes - enable KB led and restore all KB settings
			if (kbLEDSwitch) {
				
				_ledManager.Set_LEDKB_OnOff(1);
				_ledManager.SetKbBrightness(_utility.GetLastKBBacklight, kbLEDSwitch);
				_ledManager.SetKBTimeout(_utility.GetLastKBtimeout, kbLEDSwitch);
				
				int[] arrayLMR = _utility.GetLastKBleft;
				_ledManager.KBSetColorByPart(arrayLMR[0],arrayLMR[1],arrayLMR[2],"left");
				arrayLMR = _utility.GetLastKBmid;
				_ledManager.KBSetColorByPart(arrayLMR[0],arrayLMR[1],arrayLMR[2],"mid");
				arrayLMR = _utility.GetLastKBright;
				_ledManager.KBSetColorByPart(arrayLMR[0],arrayLMR[1],arrayLMR[2],"right");
				//if any effect was enabled
				if (_utility.GetLastKBeffect >0){
					_ledManager.SetKbEffect(_utility.GetLastKBeffect, true);
				}
				
			}
			//if LED KB was disabled - ensure KB LED is disabled
			else {
				_ledManager.Set_LEDKB_OnOff(0);
			}
		}
		
		
		
		/// <summary>
		/// TOOLSTRIP ITEMS CLICK HANDLERS
		/// </summary>

		//show set kb windows
		private void setKB_Click(object sender, EventArgs e)
		{
			if (manageKBswitch){
				if (this._kbWindow == null || !this._kbWindow.IsLoaded)
				{
					this._kbWindow = new KBWindow(this._ledManager, this._utility, this._osd);
					this._kbWindow.Closing += delegate(object s, CancelEventArgs args)
					{
						this._kbWindow = null;
					};
				}
				this._kbWindow.Show();
				_kbWindow.UpdateUI();
			}
			else {
				this._notifyIcon.ShowBalloonTip(1000, "KB Manager ", string.Format("Enable KB Management setting "),ToolTipIcon.Error);
			}
		}
		
		//handler for clicking menu items by color
		private void setLogo_ClickByColor(object sender, EventArgs e)
		{
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			int logocolorcode = (int)clickeditem.Tag;
			_ledManager.SetLogoWmi(logocolorcode);
			_utility.SaveLastLogo(logocolorcode);
			_utility.SaveLogoRGBonORoff(false);
		}
		
				//handler for kbmanager 
		private void EnableKBManage_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			if (clickeditem.Checked) {
				clickeditem.Checked = false;
				_utility.SetManageKB (false);
				manageKBswitch = false;
				//unregister ctrl hotkeys
				RegisterHotkeys(false);
			}
			else {
				clickeditem.Checked = true;
				_utility.SetManageKB (true);
				manageKBswitch = true;
				this.RestoreLastKBsettings();
				//register ctrl hotkeys
				RegisterHotkeys(true);
			}

		}
				
		//handler enabling silent mode in menu
		private void setSilent(object sender, EventArgs e)
		{
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			// disable silent mode
			if (clickeditem.Checked) {
				clickeditem.Checked = false;
				_utility.SetSilent (false);
				silentSwitch = false;
			}
			//enable silent mode
			else {
				clickeditem.Checked = true;
				_utility.SetSilent (true);
				silentSwitch = true;
			}
		}
		
		//handler autostart routine
		private void setAutostart(object sender, EventArgs e) {
			ToolStripMenuItem clickeditem = (ToolStripMenuItem)sender;
			if (clickeditem.Checked) {
				clickeditem.Checked = false;
				autostartSwitch = false;
				_utility.SetAutostart (autostartSwitch);

			}
			//enable autostart
			else {
				clickeditem.Checked = true;
				autostartSwitch = true;
				_utility.SetAutostart(autostartSwitch);
			}	
		}
		
		
		//update current logo color in menu
		private void OnContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			if (ClevoModel!=1) return;
			if (_utility.GetLastLogoRGBonORoff) {
				int [] rgb =this._utility.GetLastRGBlogo; 
				this._currentLogoLabel.Text = string.Format("Current: RGB: {0}:{1}:{2}", rgb[0],rgb[1], rgb[2]);
			}
			else {
				int colorcode = this._utility.GetLastLogo;
				this._currentLogoLabel.Text = string.Format("Current: {0}", this._ledManager.GetLogoString(colorcode));
			}

		}
		
		private void onReset_Clicked(object sender, EventArgs e)
		{
			_utility.SetAllDefaults();
			silentSwitch = _utility.GetSilent();
			autostartSwitch = _utility.GetAutostart();
			manageKBswitch = _utility.GetManageKB();
			this._menuSilent.Checked = false;
			this._menuAutostart.Checked = false;
			this._menuManageKB.Checked = false;
			try {_kbWindow.Close();} catch {}
			_kbWindow = null;
			System.Diagnostics.Process.Start(System.Reflection.Assembly.GetEntryAssembly().Location);
			this.Shutdown();
		}
		
		private void onExit_Clicked(object sender, EventArgs e)
		{
			base.Shutdown();
		}
		protected override void OnExit(ExitEventArgs e)
		{

			if (this._notifyIcon != null)
			{
				this._notifyIcon.Dispose();
			}
			if (this._components != null)
			{
				this._components.Dispose();
			}
			if(_utility.GetManageKB()) RegisterHotkeys(false);
			base.OnExit(e);
		}
		
		
		/// <summary>
		/// HOTKEYS LOGICS
		/// </summary>
		
		private void OnHKLedOnOff(HotKey _hotKeyMultiply) {
			int bright = _utility.GetLastKBBacklight;
			bool ledenabled = _utility.GetLastKBLEDstatus();
			if(bright>0 && ledenabled){
								bright=0;
				_ledManager.Set_LEDKB_OnOff(0);

				_utility.SaveLastKBBacklight(0);
				_utility.SaveLastKBLEDstatus(false);
				if (!silentSwitch) _osd.ShowKBBrightness(0);
			}
			else {
				bright=4;
			_ledManager.Set_LEDKB_OnOff(1);
			_ledManager.SetKbBrightness(4,true);
			_utility.SaveLastKBBacklight(4);
			_utility.SaveLastKBLEDstatus(true);
						if (!silentSwitch) _osd.ShowKBBrightness(100);
			}

		}
		
		private void OnHKLedTimeout(HotKey _hotKeyDivide) {
			//cycle timeout
						int timeout = _utility.GetLastKBtimeout;
						timeout++;
						if(timeout<5){
							_ledManager.SetKBTimeout(timeout,true);
							_utility.SaveLastKBtimeout(timeout);
							if (!silentSwitch) _osd.ShowKBTimeout(timeout, time[timeout]);
						}
						else {
							timeout=0;
							_ledManager.SetKBTimeout(timeout,true);
							_utility.SaveLastKBtimeout(timeout);
							if (!silentSwitch) _osd.ShowKBTimeout(timeout, time[timeout]);
						}
			
		}
		
		
		private void OnHKLedBrightnessDOWN(HotKey _hotKeySubsract) {
			int bright = _utility.GetLastKBBacklight;
			bool ledenabled = _utility.GetLastKBLEDstatus();
			
			//to avoid kb spamming and wmi freeze if kb already off
			if(bright==0) {
				if (!silentSwitch) _osd.ShowKBBrightness(bright*25);
				return;
			}
			
			// further logics
			
			if(bright>1 && ledenabled) {
				bright--;
				_ledManager.SetKbBrightness(bright,ledenabled);
				_utility.SaveLastKBBacklight(bright);
			}
			else if(bright>1 && !ledenabled) {
				bright--;
			_ledManager.Set_LEDKB_OnOff(1);
			_ledManager.SetKbBrightness(bright,true);
			_utility.SaveLastKBBacklight(bright);
			_utility.SaveLastKBLEDstatus(true);
			}
			else {
				bright =0;
					_ledManager.SetKbBrightness(bright,false);
					_utility.SaveLastKBBacklight(0);
					_utility.SaveLastKBLEDstatus(false);
			}
			if (!silentSwitch) _osd.ShowKBBrightness(bright*25);
		}
		
		private void OnHKLedBrightnessUP(HotKey _hotKeyAdd) {
			int bright = _utility.GetLastKBBacklight;
			bool ledenabled = _utility.GetLastKBLEDstatus();
			//to avoid kb spamming and wmi freeze if kb already off
			if(bright==4) {
				if (!silentSwitch) _osd.ShowKBBrightness(bright*25);
				return;
			}
			
			// further logics
			if(bright<4 && ledenabled) {
				bright++;
				_ledManager.SetKbBrightness(bright,ledenabled);
				_utility.SaveLastKBBacklight(bright);
			}
			else if(bright<4 && !ledenabled){
				bright++;
			_ledManager.Set_LEDKB_OnOff(1);
			_ledManager.SetKbBrightness(bright,true);
			_utility.SaveLastKBBacklight(bright);
			_utility.SaveLastKBLEDstatus(true);
			}
				else {
				bright=4;
					_ledManager.SetKbBrightness(bright,true);
					_utility.SaveLastKBBacklight(bright);
					_utility.SaveLastKBLEDstatus(true);
			}
			if (!silentSwitch) _osd.ShowKBBrightness(bright*25);
		}
		
		private void OnHKCoverOff(HotKey _hotKey0) {
			if (_utility.GetLastLogo !=0) {
					_ledManager.SetLogoWmi(0);
					_utility.SaveLastLogo(0);
			}
		}
		private void OnHKCoverCycle(HotKey _hotKeyDot) {
			int cycle = _utility.GetLastLogo;
			cycle++;
			if(cycle <9){
				
					_ledManager.SetLogoWmi(cycle);
					_utility.SaveLastLogo(cycle);
			}
			else {
				cycle =1;
					_ledManager.SetLogoWmi(cycle);
					_utility.SaveLastLogo(cycle);
			}
		}
			
		
				
		
		//restore cover LED after sleep mode
		private void OnPowerChange(object s, PowerModeChangedEventArgs e){
			
			switch (e.Mode){
				case PowerModes.Resume:
					this.RestoreLastLogo();
					if (manageKBswitch) this.RestoreLastKBsettings();
					 //show current KB STATE OSD
					break;
					case PowerModes.Suspend:
					break;	
			}
		}
		
		

		private Container _components;
		private NotifyIcon _notifyIcon;
		private LedManager _ledManager;
		private Utility _utility;
		private KBWindow _kbWindow;
		private OsdForm _osd;

		private ToolStripLabel _currentLogoLabel;
		
		private ToolStripMenuItem _menuBlack; 
		private ToolStripMenuItem _menuOrange;
		private ToolStripMenuItem _menuWhite;
		private ToolStripMenuItem _menuBlue;
		private ToolStripMenuItem _menuWhiteBlue;
		private ToolStripMenuItem _menuGreen;
		private ToolStripMenuItem _menuYellow;
		private ToolStripMenuItem _menuRed;
		private ToolStripMenuItem _menuPurple;
		
		private ToolStripMenuItem _menuSilent;
		private ToolStripMenuItem _menuAutostart;
		private ToolStripMenuItem _menuManageKB;
				
		private bool silentSwitch;
		private bool autostartSwitch;
		private bool kbLEDSwitch;
		private bool manageKBswitch;
		
		private int ClevoModel;
		
		private HotKey _hotKeyAdd;
		private HotKey _hotKeySubsract;
		private HotKey _hotKeyMultiply;
		private HotKey _hotKeyDivide;
		private HotKey _hotKey0;
		private HotKey _hotKeyDot;
		string[] time = {"off", "15s", "30s", "3m", "15m"};

	}
	

}
