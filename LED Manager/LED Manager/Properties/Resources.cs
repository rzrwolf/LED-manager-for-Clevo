using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Wpf.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		internal Resources()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Wpf.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Icon tray
		{
			get
			{
				return (Icon)Resources.ResourceManager.GetObject("Logo Tool", Resources.resourceCulture);
			}
		}
		
		internal static byte[] Bank_Gothic_Md_BT
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("Bank_Gothic_Md_BT", Resources.resourceCulture);
			}
		}
		
		
		internal static Bitmap bg_long
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("bg_long", Resources.resourceCulture);
			}
		}
				internal byte[] clevomof
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("clevomof", Resources.resourceCulture);
			}
		}


		internal static Bitmap osd_backlight_0
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_backlight_0", Resources.resourceCulture);
			}
		}

		internal static Bitmap osd_backlight_1
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_backlight_1", Resources.resourceCulture);
			}
		}

		internal static Bitmap osd_backlight_2
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_backlight_2", Resources.resourceCulture);
			}
		}

		internal static Bitmap osd_backlight_3
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_backlight_3", Resources.resourceCulture);
			}
		}

		internal static Bitmap osd_backlight_4
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_backlight_4", Resources.resourceCulture);
			}
		}

		internal static Bitmap osd_backlight_5
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_backlight_5", Resources.resourceCulture);
			}
		}


		internal static Bitmap osd_bar_full
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_bar_full", Resources.resourceCulture);
			}
		}

	

		internal static Bitmap osd_brightness
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_brightness", Resources.resourceCulture);
			}
		}

		internal static Bitmap osd_brightness_max
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_brightness_max", Resources.resourceCulture);
			}
		}

		internal static Bitmap osd_brightness_min
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("osd_brightness_min", Resources.resourceCulture);
			}
		}

		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;
	}
}
