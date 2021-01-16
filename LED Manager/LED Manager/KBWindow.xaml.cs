using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using LEDLib;

namespace Wpf
{

	public partial class KBWindow : Window
	{
		public KBWindow(LedManager ledmanager, Utility utility, OsdForm osd)
		{
			InitializeComponent();
			Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
			this._ledManager = ledmanager;
			this._utility = utility;
			this._osd = osd;
			this.colorPicker = new ColorBox();
			
			silentswitch = _utility.GetSilent();
			ClevoModel = _utility.GetModel();
			
			SetInterface();
			RestoreLastSettings ();
			SubscribeToEvents();
		}
		

		
		
		//instead of closing window - hide it to avoid multiple calls to WMI
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
		
	

		private void SetInterface() {
			
		
			ComboBoxItem backlightLevel0 = new ComboBoxItem();
			ComboBoxItem backlightLevel1 = new ComboBoxItem();
			ComboBoxItem backlightLevel2 = new ComboBoxItem();
			ComboBoxItem backlightLevel3 = new ComboBoxItem();
			ComboBoxItem backlightLevel4 = new ComboBoxItem();
			backlightLevel0.Content = "KB LED Backlight Off";
			backlightLevel1.Content = "KB LED Backlight 25%";
			backlightLevel2.Content = "KB LED Backlight 50%";
			backlightLevel3.Content = "KB LED Backlight 75%";	
			backlightLevel4.Content = "KB LED Backlight 100%";
			
			
			backlightSetBox.Items.Add(backlightLevel0);
			backlightSetBox.Items.Add(backlightLevel1);
			backlightSetBox.Items.Add(backlightLevel2);
			backlightSetBox.Items.Add(backlightLevel3);
			backlightSetBox.Items.Add(backlightLevel4);
			
			ComboBoxItem backlightTimeout0 = new ComboBoxItem();
			ComboBoxItem backlightTimeout1 = new ComboBoxItem();
			ComboBoxItem backlightTimeout2 = new ComboBoxItem();
			ComboBoxItem backlightTimeout3 = new ComboBoxItem();
			ComboBoxItem backlightTimeout4 = new ComboBoxItem();
			
			backlightTimeout0.Content = backlightTimeoutString[0];
			backlightTimeout1.Content = backlightTimeoutString[1];
			backlightTimeout2.Content = backlightTimeoutString[2];
			backlightTimeout3.Content = backlightTimeoutString[3];
			backlightTimeout4.Content = backlightTimeoutString[4];
			
			
			backlightTimeoutBox.Items.Add(backlightTimeout0);
			backlightTimeoutBox.Items.Add(backlightTimeout1);
			backlightTimeoutBox.Items.Add(backlightTimeout2);
			backlightTimeoutBox.Items.Add(backlightTimeout3);
			backlightTimeoutBox.Items.Add(backlightTimeout4);
			

			ComboBoxItem logoColor0 = new ComboBoxItem();
			ComboBoxItem logoColor1 = new ComboBoxItem();
			ComboBoxItem logoColor2 = new ComboBoxItem();
			ComboBoxItem logoColor3 = new ComboBoxItem();
			ComboBoxItem logoColor4 = new ComboBoxItem();
			ComboBoxItem logoColor5 = new ComboBoxItem();
			ComboBoxItem logoColor6 = new ComboBoxItem();
			ComboBoxItem logoColor7 = new ComboBoxItem();
			ComboBoxItem logoColor8 = new ComboBoxItem();
		
			logoColor0.Content = "Cover: " + colorstring[0];
			logoColor1.Content = "Cover: " + colorstring[1];
			logoColor2.Content = "Cover: " + colorstring[2];
			logoColor3.Content = "Cover: " + colorstring[3];
			logoColor4.Content = "Cover: " + colorstring[4];
			logoColor5.Content = "Cover: " + colorstring[5];
			logoColor6.Content = "Cover: " + colorstring[6];
			logoColor7.Content = "Cover: " + colorstring[7];
			logoColor8.Content = "Cover: " + colorstring[8];
			
			backlightLogoBox.Items.Add(logoColor0);
			backlightLogoBox.Items.Add(logoColor1);
			backlightLogoBox.Items.Add(logoColor2);
			backlightLogoBox.Items.Add(logoColor3);
			backlightLogoBox.Items.Add(logoColor4);
			backlightLogoBox.Items.Add(logoColor5);
			backlightLogoBox.Items.Add(logoColor6);
			backlightLogoBox.Items.Add(logoColor7);
			backlightLogoBox.Items.Add(logoColor8);
			
			
			ComboBoxItem effectOff = new ComboBoxItem();
			ComboBoxItem effectCycle = new ComboBoxItem();
			ComboBoxItem effectWave = new ComboBoxItem();
			ComboBoxItem effectDance = new ComboBoxItem();
			ComboBoxItem effectTempo = new ComboBoxItem();
			ComboBoxItem effectFlash = new ComboBoxItem();
			ComboBoxItem effectRandomColor = new ComboBoxItem();
			ComboBoxItem effectBreath = new ComboBoxItem();
		
			effectOff.Content = "No Effects";
			effectCycle.Content = "Cycle";
			effectWave.Content = "Wave";
			effectDance.Content = "Dance";
			effectTempo.Content = "Tempo";	
			effectFlash.Content = "Flash";
			effectRandomColor.Content = "Random Color";
			effectBreath.Content = "Breath";

			kbEffectBox.Items.Add(effectOff);
			kbEffectBox.Items.Add(effectCycle);
			kbEffectBox.Items.Add(effectWave);
			kbEffectBox.Items.Add(effectDance);
			kbEffectBox.Items.Add(effectTempo);
			kbEffectBox.Items.Add(effectFlash);
			kbEffectBox.Items.Add(effectRandomColor);
			kbEffectBox.Items.Add(effectBreath);
			
			if (ClevoModel!=1) {
				backlightLogoBox.IsEnabled = false;
				setAllKBLOGO.IsEnabled = false;
				lblCover.Visibility = System.Windows.Visibility.Hidden;
				colorLabelLogo.Visibility = System.Windows.Visibility.Hidden;
				lblor.Visibility= System.Windows.Visibility.Hidden;
				
				
			}
			
		}
		
		private void SubscribeToEvents (){
			
			this._ledManager.OnLogoSet += delegate(int f, string s)
			{
				silentswitch=_utility.GetSilent();
				if (!silentswitch) _osd.ShowLogoOsd(f);
				
			};
			
			this._ledManager.OnKBSet += delegate(int f, string s)
			{
				silentswitch=_utility.GetSilent();
				if (!silentswitch) _osd.ShowKBOsd(_utility.GetLastKBleft, _utility.GetLastKBmid, _utility.GetLastKBright);
				
			};
			
			this.boxRedL.TextChanged += ColorBox_TextChanged;
			this.boxRedM.TextChanged += ColorBox_TextChanged;
			this.boxRedR.TextChanged += ColorBox_TextChanged;
			this.boxGreenL.TextChanged += ColorBox_TextChanged;
			this.boxGreenM.TextChanged += ColorBox_TextChanged;
			this.boxGreenR.TextChanged += ColorBox_TextChanged;
			this.boxBlueL.TextChanged += ColorBox_TextChanged;
			this.boxBlueM.TextChanged += ColorBox_TextChanged;
			this.boxBlueR.TextChanged += ColorBox_TextChanged;
		}
		
		private void RestoreLastSettings (){
			
			RestoreLastKBsettings();
			RestoreLastLogoSettings();

			WriteColorBoxes(false, false);
			UpdateColorLabels();
			
		}
		
		public void UpdateUI() {
			this.backlightSetBox.SelectedIndex=_utility.GetLastKBBacklight;
			this.backlightTimeoutBox.SelectedIndex=_utility.GetLastKBtimeout;
			if(!_utility.GetLastLogoRGBonORoff) this.backlightLogoBox.SelectedIndex = _utility.GetLastLogo;
		}
		
		private void RestoreLastKBsettings (){
			
			//just in case - not only write, but WMI set last KB colors
					int[] rgbarray = _utility.GetLastKBleft;
					redL = rgbarray [0];
					greenL = rgbarray [1];
					blueL = rgbarray [2];
					_ledManager.KBSetColorByPart(rgbarray[0],rgbarray[1],rgbarray[2],"left");
					
					rgbarray = _utility.GetLastKBmid;
					redM = rgbarray [0];
					greenM = rgbarray [1];
					blueM = rgbarray [2];
					_ledManager.KBSetColorByPart(rgbarray[0],rgbarray[1],rgbarray[2],"mid");
					
					rgbarray = _utility.GetLastKBright;
					redR= rgbarray [0];
					greenR = rgbarray [1];
					blueR = rgbarray [2];
					_ledManager.KBSetColorByPart(rgbarray[0],rgbarray[1],rgbarray[2],"right");
					
					backlightSetBox.SelectedIndex = _utility.GetLastKBBacklight;
					backlightTimeoutBox.SelectedIndex = _utility.GetLastKBtimeout;	
					kbEffectBox.SelectedIndex =_utility.GetLastKBeffect;
					if (_utility.GetLastKBeffect >0){
					_ledManager.SetKbEffect(_utility.GetLastKBeffect, true);
					}
				}
	
		
		private void RestoreLastLogoSettings (){
			int[] lastLogoRGB = _utility.GetLastRGBlogo;
			redLogo = lastLogoRGB[0];
			greenLogo = lastLogoRGB[1];
			blueLogo = lastLogoRGB[2];
			if (_utility.GetLastLogoRGBonORoff) {
				ComboBoxItem logoColor9 = new ComboBoxItem();
				logoColor9.Content = "Cover: RGB:" +redLogo+":"+greenLogo+":"+blueLogo;
				backlightLogoBox.Items.Add(logoColor9);
				backlightLogoBox.SelectedIndex = 9;
			}
			else {
				backlightLogoBox.SelectedIndex =_utility.GetLastLogo;
			}
		}
		
		//refresh labels with color sample
		private void UpdateColorLabels (){
				colorLabelLeft.Background = new SolidColorBrush(Color.FromRgb((byte)redL,(byte)greenL, (byte)blueL) );
				colorLabelMid.Background = new SolidColorBrush(Color.FromRgb((byte)redM,(byte)greenM, (byte)blueM) );
				colorLabelRight.Background = new SolidColorBrush(Color.FromRgb((byte)redR,(byte)greenR, (byte)blueR) );
				colorLabelLogo.Background = new SolidColorBrush(Color.FromRgb((byte)redLogo,(byte)greenLogo, (byte)blueLogo) );
		}
		
		//read current values of RGB Text boxes
		private void ReadColorBoxes () {
			redL = int.Parse(boxRedL.Text);
			greenL = int.Parse(boxGreenL.Text);
			blueL = int.Parse(boxBlueL.Text);
			redM = int.Parse(boxRedM.Text);
			greenM = int.Parse(boxGreenM.Text);
			blueM = int.Parse(boxBlueM.Text);
			redR = int.Parse(boxRedR.Text);
			greenR = int.Parse(boxGreenR.Text);
			blueR = int.Parse(boxBlueR.Text);

		}
		
		//write current variables to RGB text boxes
		private void WriteColorBoxes(bool WriteAsMiddle, bool TextChangedByUser) {
			//set flag textbox user change same as entry arg and write values to avoid REGEX messing with the values
			ChangedByUser = TextChangedByUser;
					if (!WriteAsMiddle) {
						boxRedL.Text = redL.ToString();
						boxGreenL.Text = greenL.ToString();
						boxBlueL.Text = blueL.ToString();
						boxRedM.Text = redM.ToString();
						boxGreenM.Text = greenM.ToString();
						boxBlueM.Text = blueM.ToString();
						boxRedR.Text = redR.ToString();
						boxGreenR.Text = greenR.ToString();
						boxBlueR.Text = blueR.ToString();
					}
					else {
						boxRedL.Text = redM.ToString();
						boxGreenL.Text = greenM.ToString();
						boxBlueL.Text = blueM.ToString();
						boxRedR.Text = redM.ToString();
						boxGreenR.Text = greenM.ToString();
						boxBlueR.Text = blueM.ToString();
						ReadColorBoxes ();
						UpdateColorLabels();
					}
			//set flag textbox user change as true to enable REGEX input check again
			ChangedByUser = true;
		}
		


		private void setKB_Click(object sender, RoutedEventArgs e)
		{
			ReadColorBoxes();
			UpdateColorLabels();

			//check which button was clicked by tag
			Button clickeditem = (Button)sender;
			switch(clickeditem.Tag.ToString()) {
				case "KBsetLeft":
					_utility.SaveKBleft(redL, greenL, blueL);
					_ledManager.KBSetColorByPart(redL, greenL,blueL,"left");
				break;
				case "KBsetMid":
					_utility.SaveKBmid(redM, greenM, blueM);
					_ledManager.KBSetColorByPart(redM, greenM,blueM,"mid");
				break;
				case "KBsetRight":
					_utility.SaveKBright(redR, greenR, blueR);
					_ledManager.KBSetColorByPart(redR, greenR,blueR,"right");
				break;
				case "KBsetAll":
					_utility.SaveKBleft(redM, greenM, blueM);
					_utility.SaveKBmid(redM, greenM, blueM);
					_utility.SaveKBright(redM, greenM, blueM);
					
					_ledManager.KBSetColorByPart(redM, greenM,blueM,"left");
					_ledManager.KBSetColorByPart(redM, greenM,blueM,"mid");
					_ledManager.KBSetColorByPart(redM, greenM,blueM,"right");

					WriteColorBoxes(true, false);	
					UpdateColorLabels();
					ChangedByUser = false;
					kbEffectBox.SelectedIndex= 0;
					ChangedByUser = true;
				break;
				case "KBsetAllCover":
						_utility.SaveKBleft(redM, greenM, blueM);
						_utility.SaveKBmid(redM, greenM, blueM);
						_utility.SaveKBright(redM, greenM, blueM);
						_utility.SaveRGBlogo(redM, greenM, blueM);
						
						_utility.SaveLogoRGBonORoff (true);
						AddLogoRGBDescription();
						//equalize middle and logo variables after setting KB+LOGO for color label update
						redLogo = redM;
						greenLogo = greenM;
						blueLogo = blueM;
						_ledManager.KBSetColorByPart(redM, greenM,blueM,"left");
						_ledManager.KBSetColorByPart(redM, greenM,blueM,"mid");
						_ledManager.KBSetColorByPart(redM, greenM,blueM,"right");
						_ledManager.KBSetColorByPart(redM, greenM,blueM,"cover");
						_utility.SaveLastLogo(9);

						//write/update
						WriteColorBoxes(true, false);
						UpdateColorLabels();
						ChangedByUser = false;
						kbEffectBox.SelectedIndex= 0;
						ChangedByUser = true;
				break;		
			}
		}
					
		
				private void backlightSetBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				ComboBox clickeditem1 = (ComboBox)sender;
				int index = clickeditem1.SelectedIndex;
				if (index == _utility.GetLastKBBacklight) {

				}
				else {
					
				_ledManager.SetKbBrightness(index, _utility.GetLastKBLEDstatus());
				_utility.SaveLastKBBacklight(index);
				
				if (silentswitch==false) _osd.ShowKBBrightness(index*25);
					if (index ==0) 
					{
						_utility.SaveLastKBLEDstatus(false);
					}
					else {
						_utility.SaveLastKBLEDstatus(true);
					}
				}

				
		}
				
						
				private void backlightTimeoutBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				ComboBox clickeditem2 = (ComboBox)sender;
				int index = clickeditem2.SelectedIndex;
				if (index !=_utility.GetLastKBtimeout) {
					_utility.SaveLastKBtimeout(index);	
					_ledManager.SetKBTimeout(index, _utility.GetLastKBLEDstatus());
					if (silentswitch==false) _osd.ShowKBTimeout(index, time[index]);
				}
		}
				
				
				//some pretty fucked up logic for logo box - including last RGB if was used - dont ask how it works
				private void backlightLogoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				ComboBox clickeditem3 = (ComboBox)sender;
				int index = clickeditem3.SelectedIndex;
				int lastlogo = _utility.GetLastLogo;
				
				//if same logo selected - try to clean RGB entry & return
				if (index == lastlogo && index != 9) {
					try {backlightLogoBox.Items.RemoveAt(9);} catch {}
					return;
				}
				
				
				//when logo was actually changed
				//if black
				if (index==0) {
					try {backlightLogoBox.Items.RemoveAt(9);} catch {}
					if (_utility.GetLastKBLEDstatus()) {
						_utility.SaveLastLogo(index);
						_utility.SaveLogoRGBonORoff(false);
						_ledManager.SetLogoWmi(index);

					}
					else  {
						_ledManager.SetLogoWmi(index);
						_utility.SaveLogoRGBonORoff(false);
						_ledManager.Set_LEDKB_OnOff(0);
					}
					
				}
				//if color 
				else if (index <=8 && index>0) {
							try {backlightLogoBox.Items.RemoveAt(9);} catch {}
							if (_utility.GetLastKBLEDstatus()) 
							{
								_utility.SaveLastLogo(index);
								_utility.SaveLogoRGBonORoff(false);
								_ledManager.SetLogoWmi(index);
							}
							else {
								_ledManager.SetLogoWmi(index);
								_utility.SaveLastLogo(index);
								_utility.SaveLogoRGBonORoff(false);
								_ledManager.Set_LEDKB_OnOff(0);
							}
					}
				else {
					
					}

				}
				

		
		private	void kbEffectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ChangedByUser==false) return;
				ComboBox clickeditem2 = (ComboBox)sender;
				int index = clickeditem2.SelectedIndex;
				if (index ==_utility.GetLastKBeffect) {

				}
				else {
					if (index == 0) {
						_utility.SaveLastKBeffect(index);
						RestoreLastKBsettings();

					}
					else {
						_utility.SaveLastKBeffect(index);	
						RestoreLastKBsettings();
						_ledManager.SetKbEffect(index, _utility.GetLastKBLEDstatus());

					}
				}
		}
		
				//KB timeout box closed event		
		private void backlightTimeoutBox_DropDownClosed(object sender, EventArgs e)
		{
			ComboBox clickeditem2 = (ComboBox)sender;
				int index = clickeditem2.SelectedIndex;
				if (silentswitch==false) _osd.ShowKBTimeout(index, time[index]);

		}
		
		//KBbacklight box closed event	
		private void backlightSetBox_DropDownClosed(object sender, EventArgs e)
		{
				ComboBox clickeditem2 = (ComboBox)sender;
				int index = clickeditem2.SelectedIndex;
				if (silentswitch==false) _osd.ShowKBBrightness(index*25);
		}
		
				
		void colorLabel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			
			int [] rgbarray = this.colorPicker.showColorPicker();
			if (colorPicker.colorpicked) {
				Label clickeditem = (Label)sender;
					//check which color label was clicked
					switch(clickeditem.Tag.ToString()) {
						case "Left":
							redL=rgbarray[0];
							greenL=rgbarray[1];
							blueL=rgbarray[2];
							//set and save
							_utility.SaveKBleft(redL, greenL, blueL);							
							_ledManager.KBSetColorByPart(redL, greenL,blueL,"left");

						break;
						
						case "Mid":
							redM=rgbarray[0];
							greenM=rgbarray[1];
							blueM=rgbarray[2];
							//set and save
							_utility.SaveKBmid(redM, greenM, blueM);
							_ledManager.KBSetColorByPart(redM, greenM,blueM,"mid");

						break;
							
						case "Right":
							redR=rgbarray[0];
							greenR=rgbarray[1];
							blueR=rgbarray[2];
							//set and save
							_utility.SaveKBright(redR, greenR, blueR);
							_ledManager.KBSetColorByPart(redR, greenR,blueR,"right");

						break;
						
						case "Cover":
							redLogo=rgbarray[0];
							greenLogo=rgbarray[1];
							blueLogo=rgbarray[2];
							colorLabelLogo.Background = new SolidColorBrush(Color.FromRgb((byte)redLogo,(byte)greenLogo, (byte)blueLogo) );
							//set and save
							_utility.SaveRGBlogo(redLogo, greenLogo, blueLogo);
							_utility.SaveLogoRGBonORoff(true);
							_ledManager.KBSetColorByPart(redLogo, greenLogo,blueLogo,"cover");
							AddLogoRGBDescription();
							if(!silentswitch) _osd.ShowLogoOsd (9, redLogo, greenLogo, blueLogo);
						break;
					}	
				
				WriteColorBoxes(false, false);
				UpdateColorLabels();
			}
		}
		

		private void AddLogoRGBDescription () {
			//check if previous RGB description was added into logo box list
				try {backlightLogoBox.Items.RemoveAt(9);}
				catch {}
				ComboBoxItem logoColor9 = new ComboBoxItem();
				logoColor9.Content = "Cover: RGB:" +redLogo+":"+greenLogo+":"+blueLogo;
				backlightLogoBox.Items.Add(logoColor9);
				backlightLogoBox.SelectedIndex = 9;
		}
				
				
		
		//  validation for color text boxes
		void ColorBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(ChangedByUser) {
				TextBox clickeditem = (TextBox)sender;
				Regex regex = new Regex("^[0-9]{1,3}$");
	
				if (regex.IsMatch(clickeditem.Text)){
					if(int.Parse(clickeditem.Text) <=255) {
						textbackup = clickeditem.Text;
					}
					else {
						clickeditem.Text = textbackup;
						clickeditem.SelectAll();
					}
				}
				else {
					clickeditem.Text = textbackup;
					clickeditem.SelectAll();
				}
				ReadColorBoxes();
				UpdateColorLabels();
			}

		}		

		
			

		void buttonDefaults_Click(object sender, RoutedEventArgs e)
		{
						redL = 0;
						greenL = 0;
						blueL = 255;
						redM = 0;
						greenM = 0;
						blueM = 255;
						redR = 0;
						greenR = 0;
						blueR = 255;
						redLogo = 0;
						greenLogo = 0;
						blueLogo = 255;
						
						
						_utility.SaveLogoRGBonORoff (false);
						_utility.SaveKBleft(0, 0, 255);
						_utility.SaveKBmid(0, 0, 255);
						_utility.SaveKBright(0, 0, 255);
						_utility.SaveRGBlogo(0, 0, 255);
						_utility.SaveLastLogo(4);
						
						_utility.SaveLastKBtimeout(0);
						_utility.SaveLastKBeffect(0);
						_utility.SaveLastKBBacklight(4);
						
						_ledManager.KBSetColorByPart(0, 0,255,"left");
						_ledManager.KBSetColorByPart(0, 0,255,"mid");
						_ledManager.KBSetColorByPart(0, 0,255,"right");
						_ledManager.SetLogoWmi(3);
						
						backlightSetBox.SelectedIndex = 4;
						backlightTimeoutBox.SelectedIndex = 0;	
						kbEffectBox.SelectedIndex =0;
						backlightLogoBox.SelectedIndex = 3;
						
						
						//write/update
						try {backlightLogoBox.Items.RemoveAt(9);}
						catch {}
						WriteColorBoxes(false, false);
						UpdateColorLabels();
		}
		
		
		void buttonExit_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
		}
		
		
		

		
		//events for more usefull gui

		
		private void boxRedL_GotMouseCapture(object sender, MouseEventArgs e)
		{boxRedL.SelectAll();
		textbackup=boxRedL.Text;}
		private void boxRedM_GotMouseCapture(object sender, MouseEventArgs e)
		{boxRedM.SelectAll();
		textbackup=boxRedM.Text;}
		private void boxRedR_GotMouseCapture(object sender, MouseEventArgs e)
		{boxRedR.SelectAll();
		textbackup=boxRedR.Text;}
		private void boxBlueL_GotMouseCapture(object sender, MouseEventArgs e)
		{boxBlueL.SelectAll();
		textbackup=boxBlueL.Text;}
		private void boxBlueM_GotMouseCapture(object sender, MouseEventArgs e)
		{boxBlueM.SelectAll();				
		textbackup=boxBlueM.Text;}	
		private void boxBlueR_GotMouseCapture(object sender, MouseEventArgs e)
		{boxBlueR.SelectAll();
		textbackup=boxBlueR.Text;}	
		private void boxGreenL_GotMouseCapture(object sender, MouseEventArgs e)
		{boxGreenL.SelectAll();				
		textbackup=boxGreenL.Text;}
		private void boxGreenM_GotMouseCapture(object sender, MouseEventArgs e)
		{boxGreenM.SelectAll();				
		textbackup=boxGreenM.Text;}
		private void boxGreenR_GotMouseCapture(object sender, MouseEventArgs e)
		{boxGreenR.SelectAll();				
		textbackup=boxGreenR.Text;}
				
		//class variables and objects

		private int redL;
		private int greenL;
		private int blueL;
		private int redM;
		private int greenM;
		private int blueM;
		private int redR;
		private int greenR;
		private int blueR;
		private int redLogo;
		private int greenLogo;
		private int blueLogo;
		
		private LedManager _ledManager;
		private Utility _utility;
		private OsdForm _osd;
		private ColorBox colorPicker;
		private bool silentswitch;
		string[] time = {"off", "15s", "30s", "3m", "15m"};
		string[] backlightTimeoutString = {"KB LED Timeout Off","KB LED Timeout 15s", "KB LED Timeout 30s", "KB LED Timeout 3m", "KB LED Timeout 15m"};
		private string[] colorstring = {"Black (disabled)", "White", "Orange", "Blue", "White Blue", "Green", "Yellow", "Red", "Purple"};
		private string textbackup;
		private bool ChangedByUser = true;
		private int ClevoModel;


		
		

		
	}
}