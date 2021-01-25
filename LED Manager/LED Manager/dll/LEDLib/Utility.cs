
using System;
using Microsoft.Win32;

namespace LEDLib

{

	public class Utility
	{
		public Utility()
		{
		}
		//
		//GENERAL functions
		//
		
		public int GetModel(){
			//do some stuff to check model
			
			
			//save
			return this._settings.Model;
			
		}
		
		public void SetModel(int model){
			//do some stuff to check model
			
			
			//save
			this._settings.Model = model;
			
		}
		
		public bool GetAutostart()
		{
			
			if ( this._settings.Autostart == true) {
					//FOOLPROOF?? check if autostart correctly enabled in registry for exe 
					//get path
					_execPath = System.Reflection.Assembly.GetEntryAssembly().Location;
						try{
	
						RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
						string value = rk.GetValue("LEDmanager").ToString();
		
								if (_execPath==value) {
									return	this._settings.Autostart;		
								}
								else {
									System.Diagnostics.Debug.WriteLine("path not the same, rewriting");
									rk.DeleteValue("LEDmanager", false);
									rk.SetValue("LEDmanager", _execPath);
									return	this._settings.Autostart;
								}
						
						} catch (Exception) {
							return	false;
						}
			
			}
			
			else {
				return	this._settings.Autostart;
			}
			
		}

		
		public bool SetAutostart(bool autostart)
		{
			this._settings.Autostart = autostart;
			_execPath = System.Reflection.Assembly.GetEntryAssembly().Location;
			
			// disable autostart
			if(autostart==false){
				try{
								
					RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
					rk.DeleteValue("LEDmanager",false);
					return true;
					} catch (Exception) {
						return false;
					}
				}
			//enable autostart
			else {

				try{

					RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
					rk.SetValue("LEDmanager", _execPath);
					return true;
					} catch (Exception) {
						return false;
					}
			}
			}
		
		
		
		//no OSD/notifications	
		public bool GetSilent()
		{
			return	this._settings.Silent;
		}

		public void SetSilent(bool silent)
		{
			this._settings.Silent = silent;
		}
		
		
		//setup and mangagement kb settings enabled or not
		public bool GetManageKB()
		{
			return	this._settings.ManageKB;
		}

		public void SetManageKB (bool manage)
		{
			this._settings.ManageKB = manage;
		}
		
		
		//check if KB LED was enabled or not before in settings
		public bool GetLastKBLEDstatus()
		{
			return	this._settings.KBenabledLED;
		}

		public void SaveLastKBLEDstatus(bool enabled)
		{
			this._settings.KBenabledLED = enabled;
		}
		

			
		//
		
		//LED RELATED functions
		//
		
				
		
		//get last saved logo
		public int GetLastLogo
		{
			get
			{
				return this._settings.LastLogo;
			}
		}
		
		public void SaveLastLogo (int logo)
		{

				this._settings.LastLogo = logo;
		}
		
		
		

		//Save last KB RGB setup
		public void SaveKBleft (int r, int g, int b)
		{

				this._settings.LastKBredL = r;
				this._settings.LastKBgreenL = g;
				this._settings.LastKBblueL = b;
		}
		
		public void SaveKBmid (int r, int g, int b)
		{

				this._settings.LastKBredM = r;
				this._settings.LastKBgreenM = g;
				this._settings.LastKBblueM = b;
		}
		
		public void SaveKBright (int r, int g, int b)
		{

				this._settings.LastKBredR = r;
				this._settings.LastKBgreenR = g;
				this._settings.LastKBblueR = b;
		}
		
		
		//Get arrays of last RGB settings
		public int[] GetLastKBleft
		{
			get
			{
				int[] rgbarray = {this._settings.LastKBredL, this._settings.LastKBgreenL, this._settings.LastKBblueL};
				return rgbarray;
			}
		}
		
		public int[] GetLastKBmid
		{
			get
			{
				int[] rgbarray = {this._settings.LastKBredM, this._settings.LastKBgreenM, this._settings.LastKBblueM};
				return rgbarray;
			}
		}
						
		public int[] GetLastKBright
		{
			get
			{
				int[] rgbarray = {this._settings.LastKBredR, this._settings.LastKBgreenR, this._settings.LastKBblueR};
				return rgbarray;
			}
		}
				
		
		//backlight
				public int GetLastKBBacklight
		{
			get
			{
				return this._settings.LastKBbacklight;
			}
		}
		
		public void SaveLastKBBacklight (int level)
		{
			this._settings.LastKBbacklight = level;
			if (level == 0) {
				this._settings.KBenabledLED = false;

			}
			else{
				this._settings.KBenabledLED = true;
			}
		}
		
		//timeout backlight
		
		
		public int GetLastKBtimeout
		{
			get
			{
				return this._settings.LastKBtimeout;
			}
		}
		
		public void SaveLastKBtimeout (int timeout)
		{

				this._settings.LastKBtimeout = timeout;
		}
		
		
		//RGB LOGO
		public bool GetLastLogoRGBonORoff
		{
			get
			{
				return this._settings.LastLogoRGBonORoff;
			}
		}
		
		public void SaveLogoRGBonORoff (bool enabled)
		{

				this._settings.LastLogoRGBonORoff = enabled;
		}
		
		public void SaveRGBlogo (int r, int g, int b)
		{

				this._settings.LastLogoR = r;
				this._settings.LastLogoG = g;
				this._settings.LastLogoB = b;
		}
		
		public int[] GetLastRGBlogo
		{
			get
			{
				int[] rgbarray = {this._settings.LastLogoR, this._settings.LastLogoG, this._settings.LastLogoB};
				return rgbarray;
			}
		}
				
		
		//KB EFFECTS	
		public int GetLastKBeffect
		{
			get
			{
				return this._settings.LastKBeffect;
			}
		}
		
		public void SaveLastKBeffect (int effect)
		{

				this._settings.LastKBeffect = effect;
		}
		
		public void SetAllDefaults (){

			this._settings.Silent = false;
			this._settings.ManageKB = false;
			this.SetAutostart(false);
			this._settings.KBenabledLED = true;
			this._settings.LastLogo = 3;

			this._settings.LastKBredL = 0;
			this._settings.LastKBgreenL = 0;
			this._settings.LastKBblueL = 255;
			this._settings.LastKBredM = 0;
			this._settings.LastKBgreenM = 0;
			this._settings.LastKBblueM = 255;
			this._settings.LastKBredR = 0;
			this._settings.LastKBgreenR = 0;
			this._settings.LastKBblueR = 255;

			this._settings.LastKBbacklight = 4;
			this._settings.LastKBtimeout = 0;

			this._settings.LastLogoRGBonORoff = false;
			this._settings.LastLogoR = 0;
			this._settings.LastLogoG = 0;
			this._settings.LastLogoB = 255;
			this._settings.LastKBeffect = 0;
			this.SetModel(0);
		}
		

		
		private SettingStore _settings = new SettingStore();
		
		private string _execPath;
}
}