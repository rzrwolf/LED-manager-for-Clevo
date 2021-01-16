namespace Wpf
{
	public partial class OsdForm : global::System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			this.timer2 = new global::System.Windows.Forms.Timer(this.components);
			this.pic = new global::System.Windows.Forms.PictureBox();
			this.elementHost1 = new global::System.Windows.Forms.Integration.ElementHost();
			this.T_value = new global::System.Windows.Forms.Label();
			this.pic_bar = new global::System.Windows.Forms.PictureBox();
			((global::System.ComponentModel.ISupportInitialize)this.pic).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.pic_bar).BeginInit();
			base.SuspendLayout();
			this.timer1.Interval = 2500;
			this.timer1.Tick += new global::System.EventHandler(this.timer1_Tick);
			this.timer2.Interval = 50;
			this.timer2.Tick += new global::System.EventHandler(this.timer2_Tick);
			this.pic.BackColor = global::System.Drawing.Color.Transparent;
			this.pic.Image = global::Wpf.Properties.Resources.osd_backlight_0;
			this.pic.Location = new global::System.Drawing.Point(0, 0);
			this.pic.Margin = new global::System.Windows.Forms.Padding(2);
			this.pic.Name = "pic";
			this.pic.Size = new global::System.Drawing.Size(429, 90);
			this.pic.TabIndex = 0;
			this.pic.TabStop = false;
			this.elementHost1.BackColor = global::System.Drawing.Color.Black;
			this.elementHost1.Location = new global::System.Drawing.Point(0, 0);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new global::System.Drawing.Size(1, 1);
			this.elementHost1.TabIndex = 1;
			this.elementHost1.Text = "elementHost1";
			this.elementHost1.Visible = false;
			this.elementHost1.Child = null;
			this.T_value.AutoEllipsis = true;
			this.T_value.BackColor = global::System.Drawing.Color.Transparent;
			this.T_value.Font = new global::System.Drawing.Font("Arial", 15.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			this.T_value.ForeColor = global::System.Drawing.Color.White;
			this.T_value.Location = new global::System.Drawing.Point(347, 0);
			this.T_value.Name = "T_value";
			this.T_value.Size = new global::System.Drawing.Size(83, 90);
			this.T_value.TabIndex = 2;
			this.T_value.Text = "100";
			this.T_value.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.pic_bar.BackColor = global::System.Drawing.Color.Transparent;
			this.pic_bar.Image = global::Wpf.Properties.Resources.osd_bar_full;
			this.pic_bar.Location = new global::System.Drawing.Point(107, 0);
			this.pic_bar.Margin = new global::System.Windows.Forms.Padding(2);
			this.pic_bar.Name = "pic_bar";
			this.pic_bar.Size = new global::System.Drawing.Size(240, 90);
			this.pic_bar.TabIndex = 3;
			this.pic_bar.TabStop = false;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = global::System.Drawing.Color.Black;
			base.ClientSize = new global::System.Drawing.Size(429, 90);
			base.ControlBox = false;
			base.Controls.Add(this.T_value);
			base.Controls.Add(this.pic_bar);
			base.Controls.Add(this.elementHost1);
			base.Controls.Add(this.pic);
			this.DoubleBuffered = true;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Location = new global::System.Drawing.Point(50, 60);
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OsdForm";
			base.Opacity = 0.9;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "OsdForm";
			((global::System.ComponentModel.ISupportInitialize)this.pic).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.pic_bar).EndInit();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Timer timer1;

		private global::System.Windows.Forms.Timer timer2;

		private global::System.Windows.Forms.PictureBox pic;

		private global::System.Windows.Forms.Integration.ElementHost elementHost1;

		private global::System.Windows.Forms.Label T_value;

		private global::System.Windows.Forms.PictureBox pic_bar;
	}
}
