
using System;
using System.Windows.Forms;

namespace Wpf
{

	public class ColorBox
	{
		public ColorBox()
		{
			
			
			this.colorBox = new ColorDialog();
			colorBox.FullOpen = true;


			
		}
		
		public int[] showColorPicker () {
		colorpicked = false;
			if (this.colorBox.ShowDialog()==System.Windows.Forms.DialogResult.OK) {
				int [] color = {colorBox.Color.R, colorBox.Color.G, colorBox.Color.B};
				colorpicked = true;
				return color;
			}
			else {
				int [] color = {256,256,256}; 
								colorpicked = false;
				return color;

			
				}
			}
		
		private ColorDialog colorBox;
		public bool colorpicked;
		
	}
}
