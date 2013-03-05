using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	internal class FormColours : System.Windows.Forms.Form
	{
		#region Class Fields
		private System.ComponentModel.Container components = null;
		private FormMain _parent = null;
		private TemperatureScales _temperaturescale;
		#endregion

		#region Constructor
		internal FormColours(FormMain frm, TemperatureScales temperaturescale)
		{
			InitializeComponent();

			_parent = frm;
			_temperaturescale = temperaturescale;
		}
		#endregion

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormColours));
			// 
			// FormColours
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(224, 414);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormColours";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Tag = "running-colourscheme.htm";
			this.Text = "Colours";
			this.Load += new System.EventHandler(this.FormColours_Load);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FormColours_HelpRequested);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormColours_Paint);

		}
		#endregion

		#region Events
		private void FormColours_Load(object sender, System.EventArgs e)
		{
			// Set the width
			if(_temperaturescale == TemperatureScales.Celsius)
			{
				this.Width = Constants.COLOUR_WINDOW_WIDTH_C;
			}
			else
			{
				this.Width = Constants.COLOUR_WINDOW_WIDTH_F;
			}
		}

		private void FormColours_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.FormColours);
		}

		private void FormColours_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			IconPair ip;
			Graphics g = e.Graphics;
			g.DrawRectangle(Pens.Black, 0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);

			int column = 0;
			int row    = 0;

			// Do the temperatures
			int templower, tempupper;
			if(_temperaturescale == TemperatureScales.Farenheit)
			{
				templower = -4;
				tempupper = 104;
			}
			else
			{
				templower = -20;
				tempupper = 40;
			}

			for(int i = templower; i < tempupper; i++)
			{
				// TODO: sort out the mapping between image placement and temp
				ip = _parent.MakeIcons(i.ToString());
				g.DrawImageUnscaled(ip.OffIcon.ToBitmap(), column * 20 + 4,  row * 20 + 4);
				g.DrawImageUnscaled(ip.OnIcon.ToBitmap(),  column * 20 + 24, row * 20 + 4);
				row++;
				if(row == 20)
				{
					column += 3;
					row = 0;
				}
      }
			
			// Do the special characters
			ip = _parent.MakeIcons(Constants.CHAR_BADPOSTCODE);
			g.DrawImageUnscaled(ip.OffIcon.ToBitmap(), column * 20 + 4,  row * 20 + 4);
			g.DrawImageUnscaled(ip.OnIcon.ToBitmap(),  column * 20 + 24, row * 20 + 4);

			row++;
			ip = _parent.MakeIcons(Constants.CHAR_NONETWORK);
			g.DrawImageUnscaled(ip.OffIcon.ToBitmap(), column * 20 + 4,  row * 20 + 4);
			g.DrawImageUnscaled(ip.OnIcon.ToBitmap(),  column * 20 + 24, row * 20 + 4);

			row++;
			ip = _parent.MakeIcons(Constants.CHAR_OBTAININGDATA);
			g.DrawImageUnscaled(ip.OffIcon.ToBitmap(), column * 20 + 4,  row * 20 + 4);
			g.DrawImageUnscaled(ip.OnIcon.ToBitmap(),  column * 20 + 24, row * 20 + 4);

			row++;
			ip = _parent.MakeIcons(Constants.CHAR_ODDDATA);
			g.DrawImageUnscaled(ip.OffIcon.ToBitmap(), column * 20 + 4,  row * 20 + 4);
			g.DrawImageUnscaled(ip.OnIcon.ToBitmap(),  column * 20 + 24, row * 20 + 4);
		}
		#endregion
	}
}
