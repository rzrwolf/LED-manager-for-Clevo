using System;

namespace LEDLib
{
	public class LedManager
	
	{
		public event LedManager.LogoEventHandler OnLogoSet;

		
		public event LedManager.KBEventHandler OnKBSet;




		public LedManager()
		{
			this.OnLogoSet += delegate(int f, string s)
			{
			};
			
			this.OnKBSet += delegate(int f, string s)
			{
			};
			
			this.wmi = new WMIcontrol();
			this.wmi.InitWMI();
		}
		

		//Settings KB LED RGB by each part		
		public void KBSetColorByPart(int _iR, int _iG, int _iB, string KbPart)
		{

			int num = (int)Math.Round((double)_iB * 200.0 / 255.0);
			long num2 = (long)((_iB << 16) + _iG + (_iR << 8));
			
			if (KbPart == "left")
			{
				num2 = (long)((num << 16) + _iG + (_iR << 8));
				num2 |= 4026531840u;

			}
			else if (KbPart == "mid")
			{
				num2 = (num << 16) + _iG + (_iR << 8);
				num2 |= 4043309056u;
			}
			else if (KbPart == "right")
			{
				num2 = (long)((num << 16) + _iG + (_iR << 8));
				num2 |= 4060086272u;
			}
			
			//cover switching - supports RGB colors (?)
			else if (KbPart == "cover")
			{
				if (_iB == 0 && _iG == 147 && _iR == 255)
				{
					num2 = 51300L;
				}
				else
				{
					num2 = (long)((_iB / 2 << 16) + _iG / 2 + (_iR / 2 << 8));
				}
				SetLogoOnOff(0);
				this.wmi.InvokeMethod(wmiDisableLOGOsetcolor1, wmiCommandKBLED);
				num2 |= 4127195136u;
				this.wmi.InvokeMethod(num2.ToString(), wmiCommandKBLED);
				this.wmi.InvokeMethod(wmiEnableLOGOsetcolor2, wmiCommandKBLED);
				SetLogoOnOff(1);
				return;
			}
			
			this.wmi.InvokeMethod(num2.ToString(), wmiCommandKBLED);
			//make KB event
			this.OnKBSet(1, KbPart);

		}
		
			
		//enable-disable LED KB with LOGO
		public  void Set_LEDKB_OnOff(int status)
		{
			switch (status) {
			case 0:
				this.wmi.InvokeMethod(wmiDisableKBcode, wmiCommandKBLED);
			break;
			
			case 1:
				this.wmi.InvokeMethod(wmiEnableKBcode, wmiCommandKBLED);
			break;
			}
		}
		
		//enable-disable LOGO only
		public void SetLogoOnOff(int status)
		{
			switch (status) {
			case 0:
				this.wmi.InvokeMethod(wmiDisableLOGOcode, wmiCommandKBLED);
			break;
			
			case 1:
				this.wmi.InvokeMethod(wmiEnableLOGOcode, wmiCommandKBLED);
			break;
			}
		}
			
		
		//separate logo set function by quick color preset
			public void SetLogoWmi(int colorcode)
		{
				//set logo off
				SetLogoOnOff(0);
				this.wmi.InvokeMethod(wmiDisableLOGOsetcolor1, wmiCommandKBLED);
				
				//set logo color
				this.wmi.InvokeMethod(wmicolorcodes[colorcode], wmiCommandKBLED);
				//set logo on
				this.wmi.InvokeMethod(wmiEnableLOGOsetcolor2, wmiCommandKBLED);
				SetLogoOnOff(1);
				
				this.OnLogoSet(colorcode, null);	
		}
		
		public string GetLogoString(int colorcode)
		{
			return colorstring [colorcode];
		}
		
				
		
		//setting KB+LOGO brightness (avoid multiple unnecessary wmi calls and hangups with kbLEDwasEnabled)
		public void SetKbBrightness(int index, bool kbLEDwasEnabled)
		{

			if (index ==0) {
				Set_LEDKB_OnOff(0);
				return;
			}
			else {
				if (kbLEDwasEnabled ==false) Set_LEDKB_OnOff(1);
				this.wmi.InvokeMethod(wmibrightnesscodes[index], wmiCommandKBLED);
			}
			this.OnKBSet(index, "kbBRIGHTNESS");
		}
		
		//setting KB only timeout
		public void SetKBTimeout(int index, bool kbLEDwasEnabled)
		{	

			if (kbLEDwasEnabled ==false) return;
			this.wmi.InvokeMethod(wmitimeoutcodes[index] , wmiCommandKBtimeout);
			this.OnKBSet(index, "kbEFFECT");
		}
		
		
		public void SetKbEffect (int index, bool kbLEDwasEnabled)
		{

			if (kbLEDwasEnabled ==false) return;
			if (index ==0) return;
			this.wmi.InvokeMethod(wmieffectcodes[index] , wmiCommandKBLED);
			this.OnKBSet(index, "kbTIMEOUT");
		}

		
		private WMIcontrol wmi;
		
		private const string wmiCommandKBLED = "SetKBLED"; 
		private const string wmiCommandKBtimeout = "SystemControlFunction"; 
		
		//single codes
		
		//KB disable + enable (pre-programmed WMI codes - do not edit)
		private const string wmiDisableKBcode = "3758096391";
		private const string wmiEnableKBcode = "3758616577";
		
		//Logo disable + enable - for enable disable - USE wmiDisableLOGOcode1, wmiEnableLOGOcode2
		//for color change - all 4 codes disable 1,2 - SET COLOR - enable 1, 2
		private const string wmiDisableLOGOcode = "3758608391";
		private const string wmiDisableLOGOsetcolor1 = "1073741824";
		
		private const string wmiEnableLOGOsetcolor2 = "1073807360";
		private const string wmiEnableLOGOcode = "3758616583";

		
		//wmi code arrays//
		
		//color strings for color codes quick use
		private string[] colorstring = {"Black (disabled)", "White", "Orange", "Blue", "White Blue", "Green", "Yellow", "Red", "Purple"};
		// from preset codes for quick use (can be modified by RGB functions)
		private string[] wmicolorcodes = {"4127195136","4135550847" ,"4127246436", "4135518208", "4135518335", "4127195263", "4127227775", "4127227648", "4135550720"};
		
		//WMI PRE-PROGRAMMED CODES for functions - do not edit this
		private string[] wmibrightnesscodes = {"no code disable kb", "4093640751", "4093640799", "4093640847","4093640895"};
		private string[] wmitimeoutcodes = {"402653184", "402653185", "402653186", "402653187", "402653188"};
		private string[] wmieffectcodes = {"No effects", "855703552", "2952790016", "2147483648", "2415919104", "2684354560", "1879048192", "268607488"};
		


		//events for logo and KB change
		public delegate void LogoEventHandler(int logo, string message);
		public delegate void KBEventHandler(int kb, string message);
	}
}
