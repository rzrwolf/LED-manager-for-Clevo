using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.Win32;
using Wpf.Properties;

namespace Wpf
{
	
	//mostly CCC v1.0 original code with some adjustments
	public partial class OsdForm : Form
	{
		
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		[DllImport("gdi32.dll")]
		private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

		public OsdForm()
		{

			this.InitializeComponent();
			this.bInit = true;
			this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("bg_long");

			this.pic.Image = this.BackgroundImage;
			this.rawWidth = base.Width;
			this.rawHeight = base.Height;
			byte[] bank_Gothic_Md_BT = Resources.Bank_Gothic_Md_BT;
			IntPtr intPtr = Marshal.AllocCoTaskMem(bank_Gothic_Md_BT.Length);
			Marshal.Copy(bank_Gothic_Md_BT, 0, intPtr, bank_Gothic_Md_BT.Length);
			uint num = 0U;
			this.fonts.AddMemoryFont(intPtr, Resources.Bank_Gothic_Md_BT.Length);
			OsdForm.AddFontMemResourceEx(intPtr, (uint)Resources.Bank_Gothic_Md_BT.Length, IntPtr.Zero, ref num);
			Marshal.FreeCoTaskMem(intPtr);
			Font font = new Font(this.fonts.Families[0], 16f);
			float emSize = 357.28f / font.GetHeight((float)this.GetDPI());
			this.T_value.Font = new Font(this.fonts.Families[0], emSize);
			base.Opacity = 0.0;
			this.InitTimer.Tick += this.InitTimer_Tick;
			this.InitTimer.Start();
		}

		public int GetDPI()
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop\\WindowMetrics");
			int result = (int)registryKey.GetValue("AppliedDPI");
			registryKey.Dispose();
			return result;
		}

		private void InitTimer_Tick(object sender, EventArgs e)
		{
			this.InitTimer.Stop();
			this.InitOsd();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.bInit)
			{
				base.TopMost = true;
			}
			this.timer2.Start();
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			base.Opacity -= 0.2;
			if (base.Opacity <= 0.3 && base.Opacity != 0.0)
			{
				this.pic.Image = (Image)Resources.ResourceManager.GetObject("bg_long");
				this.pic.Visible = false;
				this.pic_bar.Visible = false;
				this.T_value.Visible = false;
				base.Opacity = 0.0;
				base.Invalidate();
				OsdForm.ShowWindow(base.Handle, 4);
				return;
			}
			if (base.Opacity == 0.0)
			{
				this.timer1.Stop();
				this.timer2.Stop();
				this.bInit = false;
				OsdForm.ShowWindow(base.Handle, 0);
			}
		}

		private void InitOsd()
		{
			if (this.pic.Visible)
			{
				this.pic.Visible = false;
			}
			this.T_value.Visible = false;
			this.pic_bar.Visible = false;
			this.timer1.Stop();
			this.timer2.Stop();
			base.Opacity = 0.0;
			base.Location = new System.Drawing.Point(50, 60);
			base.Width = 429;
			base.Height = 90;
			base.Invalidate();
			base.Show();
			OsdForm.ShowWindow(base.Handle, 4);
			this.timer1.Enabled = true;
		}

		public void ShowOsd(OsdForm.OsdImage osdimage)
		{

			if (!this.pic.Visible)
			{
				this.pic.Visible = true;
			}
			this.T_value.Visible = false;
			this.pic_bar.Visible = false;
			this.timer1.Stop();
			this.timer2.Stop();
			base.Location = new System.Drawing.Point(50, 60);
			this.pic.Image = (Image)Resources.ResourceManager.GetObject(osdimage.ToString());
			this.pic.Width = this.pic.Image.Width;
			this.pic.Height = this.pic.Image.Height;
			base.Width = this.pic.Image.Width;
			base.Height = this.pic.Image.Height;
			base.Invalidate();
			base.Opacity = 0.9;
			OsdForm.ShowWindow(base.Handle, 4);
			this.timer1.Enabled = true;
		}
		
		public void ShowLogoOsd(int LogoColor, int r=0, int g =0, int b=0)
		{
			if (!this.pic.Visible)
			{
				this.pic.Visible = true;
			}
			
			
			this.T_value.Visible = true;
			this.pic_bar.Visible = false;
			this.timer1.Stop();
			this.timer2.Stop();
			base.Location = new System.Drawing.Point(50, 60);
			Bitmap imageb = new Bitmap (300,50);
			Graphics graph = Graphics.FromImage(imageb);
			if (LogoColor<9){
				graph.Clear(colorarray[LogoColor]);
			}
			else{
				graph.Clear(Color.FromArgb(r, g, b));
			}

			this.pic.Image = imageb;

			this.pic.Width = this.pic.Image.Width;
			this.pic.Height = this.pic.Image.Height;
			base.Width = this.pic.Image.Width;
			base.Height = this.pic.Image.Height;
			base.Invalidate();
			base.Opacity = 0.9;
			
			this.T_value.Text = "Cover:";
			this.T_value.Height = this.pic.Image.Height;
			this.T_value.Width = 100;
			this.T_value.Location = new System.Drawing.Point(0, 0);
			
			OsdForm.ShowWindow(base.Handle, 4);
			this.timer1.Enabled = true;
		}
		
		public void ShowKBOsd(int[] RGBArrayLeft, int[] RGBArrayMid, int[] RGBArrayRight)
		{
			
			if (!this.pic.Visible)
			{
				this.pic.Visible = true;
			}
			this.T_value.Visible = false;
			this.pic_bar.Visible = false;
			this.timer1.Stop();
			this.timer2.Stop();
			base.Location = new System.Drawing.Point(50, 60);
			Bitmap imageb = new Bitmap (300,50);
			Graphics g = Graphics.FromImage(imageb);
			g.Clear(Color.FromArgb(255,RGBArrayLeft[0], RGBArrayLeft[1], RGBArrayLeft[2]));
			g.FillRectangle(new SolidBrush(Color.FromArgb(255,RGBArrayMid[0], RGBArrayMid[1], RGBArrayMid[2])), 101, 0, 100, 50);
			g.FillRectangle(new SolidBrush(Color.FromArgb(255,RGBArrayRight[0], RGBArrayRight[1], RGBArrayRight[2])), 201, 0, 100, 50);

			this.pic.Image = imageb;

			this.pic.Width = this.pic.Image.Width;
			this.pic.Height = this.pic.Image.Height;
			base.Width = this.pic.Image.Width;
			base.Height = this.pic.Image.Height;
			base.Invalidate();
			base.Opacity = 0.9;
			OsdForm.ShowWindow(base.Handle, 4);
			this.timer1.Enabled = true;
		}


		private void ShowBarOSD(OsdForm.OsdImage osdimage, int value_max, int value, bool showBar, bool showtext, string text)
		{
			this.timer1.Stop();
			this.timer2.Stop();
			base.Location = new System.Drawing.Point(50, 60);
			this.pic.Image = (Image)Resources.ResourceManager.GetObject(osdimage.ToString());
			this.pic.Width = this.pic.Image.Width;
			this.pic.Height = this.pic.Image.Height;
			this.pic_bar.Height = this.pic.Image.Height;
			this.T_value.Height = this.pic.Image.Height;
			this.T_value.Width = 69;
			base.Width = this.pic.Image.Width;
			base.Height = this.pic.Image.Height;
			this.pic.Visible = true;
			if (showBar)
			{
				double num = 240.0 / (double)value_max * (double)value;
				if (num < 10.0)
				{
					num = 8.0;
				}
				else if (num > 230.0)
				{
					num = 240.0;
				}
				this.pic_bar.Width = (int)Math.Round(num, 0);
				this.pic_bar.Visible = true;
				if (showtext == true) {
					this.T_value.Text = text;
				}
				else {
					this.T_value.Text = value.ToString() + text;
				}
				

				this.T_value.Visible = true;
			}
			else
			{
				this.T_value.Visible = false;
				this.pic_bar.Visible = false;
			}
			this.pic_bar.Location = new System.Drawing.Point(107, 0);
			this.T_value.Location = new System.Drawing.Point(360, 0);
			base.Invalidate();
			base.Opacity = 0.9;
			OsdForm.ShowWindow(base.Handle, 4);
			this.timer1.Enabled = true;
		}
		

		public void ShowKBTimeout(int value, string time)
		{
			OsdForm.OsdImage osdimage;
			if (value == 0)
			{
				osdimage = OsdForm.OsdImage.osd_backlight_0;
			}
			else if (value == 1)
			{
				osdimage = OsdForm.OsdImage.osd_backlight_1;
			}
			else if (value == 2)
			{
				osdimage = OsdForm.OsdImage.osd_backlight_2;
			}
			else if (value == 3)
			{
				osdimage = OsdForm.OsdImage.osd_backlight_3;
			}
			else if (value == 4)
			{
				osdimage = OsdForm.OsdImage.osd_backlight_4;
			}
			else
			{
				osdimage = OsdForm.OsdImage.osd_backlight_5;
			}
			this.ShowBarOSD(osdimage, 4, value, true, true, time);
		}

		public void ShowKBBrightness(int value)
		{
			if (value == 100)
			{
				this.ShowBarOSD(OsdForm.OsdImage.osd_brightness_max, 100, value, false, false, "");
				return;
			}
			if (value == 0)
			{
				this.ShowBarOSD(OsdForm.OsdImage.osd_brightness_min, 100, value, false, false, "");
				return;
			}
			this.ShowBarOSD(OsdForm.OsdImage.osd_brightness, 100, value, true, true, value+"%");
		}

		

		private int rawWidth;
		private int rawHeight;
		private Timer InitTimer = new Timer();
		private bool bInit;
		private PrivateFontCollection fonts = new PrivateFontCollection();
		private System.Drawing.Color[] colorarray = {Color.Black, Color.White, Color.Orange, Color.Blue, Color.LightBlue, Color.Green, Color.Yellow, Color.Red, Color.Purple};

		public enum OsdImage
		{
			osd_brightness,
			osd_brightness_max,
			osd_brightness_min,
			osd_backlight_0,
			osd_backlight_1,
			osd_backlight_2,
			osd_backlight_3,
			osd_backlight_4,
			osd_backlight_5,
		}
	}
}