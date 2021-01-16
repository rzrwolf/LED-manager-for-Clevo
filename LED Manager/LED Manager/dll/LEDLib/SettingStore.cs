using System;
using LEDLib.Properties;

namespace LEDLib
{
	public class SettingStore
	{
		public int LastLogo
		{
			get
			{
				if (this._lastLogo != 0)
				{
					return this._lastLogo;
				}
				return this._lastLogo = Settings.Default.Logo;
			}
			set
			{
				Settings.Default.Logo = value;
				Settings.Default.Save();
				this._lastLogo = value;
			}
		}
		
				public bool Silent
		{
			get
			{
				if (this._silent != false)
				{
					return this._silent;
				}
				return this._silent = Settings.Default.Silent;
			}
			set
			{
				Settings.Default.Silent = value;
				Settings.Default.Save();
				this._silent = value;
			}
		}
				
						public bool ManageKB
		{
			get
			{
				if (this._ManageKB != false)
				{
					return this._ManageKB;
				}
				return this._ManageKB = Settings.Default.ManageKB;
			}
			set
			{
				Settings.Default.ManageKB = value;
				Settings.Default.Save();
				this._ManageKB = value;
			}
		}
				
				public bool Autostart
		{
			get
			{
				if (this._autostart != false)
				{
					return this._autostart;
				}
				return this._autostart = Settings.Default.Autostart;
			}
			set
			{
				Settings.Default.Autostart = value;
				Settings.Default.Save();
				this._autostart = value;
			}
		}
				
		public bool KBenabledLED
		{
			get
			{

				if (this._KBenabledLED != true)
				{
					return this._KBenabledLED;
				}
				return this._KBenabledLED = Settings.Default.KBenabledLED;
			}
			set
			{
				Settings.Default.KBenabledLED = value;
				Settings.Default.Save();
				this._KBenabledLED = value;
			}
		}
				
		
				
				/// <summary>
				/// ///LEFT SIDE
				/// </summary>
				public int LastKBredL
		{
			get
			{

				return this._LastKBredL = Settings.Default.KBredL;
			}
			set
			{
				Settings.Default.KBredL = value;
				Settings.Default.Save();
				this._LastKBredL = value;
			}
		}
				
				public int LastKBgreenL
		{
					get
			{

				return this._LastKBgreenL = Settings.Default.KBgreenL;
			}
			set
			{
				Settings.Default.KBgreenL = value;
				Settings.Default.Save();
				this._LastKBgreenL = value;
			}
		}
				
				public int LastKBblueL
		{
		get
			{

				return this._LastKBblueL = Settings.Default.KBblueL;
			}
			set
			{
				Settings.Default.KBblueL = value;
				Settings.Default.Save();
				this._LastKBblueL = value;
			}
		}
				
				
						/// <summary>
				/// ///MID SIDE
				/// </summary>
				public int LastKBredM
		{
			get
			{

				return this._LastKBredM = Settings.Default.KBredM;
			}
			set
			{
				Settings.Default.KBredM = value;
				Settings.Default.Save();
				this._LastKBredM = value;
			}
		}
				
				public int LastKBgreenM
		{
					get
			{

				return this.LastKBgreenM = Settings.Default.KBgreenM;
			}
			set
			{
				Settings.Default.KBgreenM = value;
				Settings.Default.Save();
				this._LastKBgreenM = value;
			}
		}
				
				public int LastKBblueM
		{
		get
			{

				return this._LastKBredM = Settings.Default.KBblueM;
			}
			set
			{
				Settings.Default.KBblueM = value;
				Settings.Default.Save();
				this._LastKBblueM = value;
			}
		}
				
				
		/// <summary>
				/// ///right SIDE
				/// </summary>
				public int LastKBredR
		{
			get
			{

				return this._LastKBredR = Settings.Default.KBredR;
			}
			set
			{
				Settings.Default.KBredR = value;
				Settings.Default.Save();
				this._LastKBredR = value;
			}
		}
				
				public int LastKBgreenR
		{
					get
			{

				return this.LastKBgreenR = Settings.Default.KBgreenR;
			}
			set
			{
				Settings.Default.KBgreenR = value;
				Settings.Default.Save();
				this._LastKBgreenR = value;
			}
		}
				
				public int LastKBblueR
		{
		get
			{

				return this._LastKBredR = Settings.Default.KBblueR;
			}
			set
			{
				Settings.Default.KBblueR = value;
				Settings.Default.Save();
				this._LastKBblueR = value;
			}
		}
				
				
				
				
								/// <summary>
				/// ///CUSTOM LOGO SIDE
				/// </summary>
				public int LastLogoR
		{
			get
			{

				return this._LastLogoR = Settings.Default.LogoR;
			}
			set
			{
				Settings.Default.LogoR = value;
				Settings.Default.Save();
				this._LastLogoR = value;
			}
		}
				
				public int LastLogoB
		{
			get
			{

				return this._LastLogoB = Settings.Default.LogoB;
			}
			set
			{
				Settings.Default.LogoB = value;
				Settings.Default.Save();
				this._LastLogoB = value;
			}
		}
				
			public int LastLogoG
		{
			get
			{

				return this._LastLogoG = Settings.Default.LogoG;
			}
			set
			{
				Settings.Default.LogoG = value;
				Settings.Default.Save();
				this._LastLogoG = value;
			}
		}
				
				public bool LastLogoRGBonORoff
		{
			get
			{
				if (this._LogoRGB != false)
				{
					return this._LogoRGB;
				}
				return this._LogoRGB = Settings.Default.LogoRGB;
			}
			set
			{
				Settings.Default.LogoRGB = value;
				Settings.Default.Save();
				this._LogoRGB = value;
			}
		}	
				
				
				
				
				public int LastKBbacklight
		{
		get
			{

				return this._LastKBbacklight = Settings.Default.KBbacklight;
			}
			set
			{
				Settings.Default.KBbacklight = value;
				Settings.Default.Save();
				this._LastKBbacklight = value;
			}
		}
				
				
				
				
				public int LastKBtimeout
		{
		get
			{

				return this._LastKBtimeout = Settings.Default.KBtimeout;
			}
			set
			{
				Settings.Default.KBtimeout = value;
				Settings.Default.Save();
				this._LastKBtimeout = value;
			}
		}
				
								public int LastKBeffect
		{
		get
			{

				return this._LastKBeffect = Settings.Default.KBeffect;
			}
			set
			{
				Settings.Default.KBeffect = value;
				Settings.Default.Save();
				this._LastKBeffect = value;
			}
		}
								
		public int Model
		{
		get
			{

				return this._Model = Settings.Default.ClevoModel;
			}
			set
			{
				Settings.Default.ClevoModel = value;
				Settings.Default.Save();
				this._Model = value;
			}
		}

		private int _lastLogo = 0;
		private bool _silent = false;
		private bool _autostart = false;
		private bool _KBenabledLED = true;
		private bool _ManageKB = false;
		private int _LastKBredL;
		private int _LastKBgreenL;
		private int _LastKBblueL;
		private int _LastKBredM;
		private int _LastKBgreenM;
		private int _LastKBblueM;
		private int _LastKBredR;
		private int _LastKBgreenR;
		private int _LastKBblueR;
		private int _LastKBbacklight;
		private int _LastKBtimeout;
		private int _LastKBeffect;
		private int _Model;
		
		private bool _LogoRGB;
		private int _LastLogoR;
		private int _LastLogoG;
		private int _LastLogoB;
	}
}
