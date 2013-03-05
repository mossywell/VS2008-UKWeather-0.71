using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	public class OptionsControlUi : UserControl
	{
		#region Class Fields

		internal System.Windows.Forms.RadioButton radioCelsius;
		internal System.Windows.Forms.RadioButton radioFarenheit;
		private System.Windows.Forms.Label lblTempScale;
		private Mossywell.UKWeather.UserControlTextLine userControlTextLine;
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructor
		public OptionsControlUi()
		{
			InitializeComponent();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.radioCelsius = new System.Windows.Forms.RadioButton();
			this.radioFarenheit = new System.Windows.Forms.RadioButton();
			this.lblTempScale = new System.Windows.Forms.Label();
			this.userControlTextLine = new Mossywell.UKWeather.UserControlTextLine();
			this.SuspendLayout();
			// 
			// radioCelsius
			// 
			this.radioCelsius.Location = new System.Drawing.Point(168, 32);
			this.radioCelsius.Name = "radioCelsius";
			this.radioCelsius.Size = new System.Drawing.Size(64, 16);
			this.radioCelsius.TabIndex = 3;
			this.radioCelsius.Text = "Celsius";
			// 
			// radioFarenheit
			// 
			this.radioFarenheit.Location = new System.Drawing.Point(240, 32);
			this.radioFarenheit.Name = "radioFarenheit";
			this.radioFarenheit.Size = new System.Drawing.Size(72, 16);
			this.radioFarenheit.TabIndex = 1;
			this.radioFarenheit.Text = "Farenheit";
			// 
			// lblTempScale
			// 
			this.lblTempScale.Location = new System.Drawing.Point(16, 32);
			this.lblTempScale.Name = "lblTempScale";
			this.lblTempScale.Size = new System.Drawing.Size(100, 16);
			this.lblTempScale.TabIndex = 4;
			this.lblTempScale.Text = "Temperature Scale";
			// 
			// userControlTextLine
			// 
			this.userControlTextLine.Location = new System.Drawing.Point(8, 8);
			this.userControlTextLine.Name = "userControlTextLine";
			this.userControlTextLine.Size = new System.Drawing.Size(312, 16);
			this.userControlTextLine.TabIndex = 5;
			this.userControlTextLine.TextInLine = "User Interface Options";
			// 
			// OptionsControlUi
			// 
			this.Controls.Add(this.userControlTextLine);
			this.Controls.Add(this.lblTempScale);
			this.Controls.Add(this.radioFarenheit);
			this.Controls.Add(this.radioCelsius);
			this.Name = "OptionsControlUi";
			this.Size = new System.Drawing.Size(320, 176);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
