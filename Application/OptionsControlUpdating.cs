using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	public class OptionsControlUpdating : UserControl
	{
		#region Class Fields
		// Note that we make some internal rather than using properties
		// as it's much faster and easier to code! Purists might complain...
		private System.Windows.Forms.Label lblUpdateInterval;
		internal System.Windows.Forms.TrackBar trackIconFlashFrequency;
		internal System.Windows.Forms.TextBox txtIconFlashFrequency;
		internal System.Windows.Forms.Label lblIconFlashFrequency;
		internal System.Windows.Forms.TextBox txtUpdateInterval;
		internal System.Windows.Forms.TrackBar trackUpdateInterval;
		private System.Windows.Forms.Label lblWhenWeatherChanges;
		internal System.Windows.Forms.ComboBox comboIconChanges;
		internal System.Windows.Forms.TextBox txtPostcode;
		private System.Windows.Forms.Label lblPostCode;
		private Mossywell.UKWeather.UserControlTextLine userControlTextLineIcon;
		private Mossywell.UKWeather.UserControlTextLine userControlTextLineUpdating;
		private Mossywell.UKWeather.UserControlTextLine userControlTextLinePostcode;
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructor
		public OptionsControlUpdating()
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
			this.lblUpdateInterval = new System.Windows.Forms.Label();
			this.trackIconFlashFrequency = new System.Windows.Forms.TrackBar();
			this.txtIconFlashFrequency = new System.Windows.Forms.TextBox();
			this.lblIconFlashFrequency = new System.Windows.Forms.Label();
			this.txtUpdateInterval = new System.Windows.Forms.TextBox();
			this.trackUpdateInterval = new System.Windows.Forms.TrackBar();
			this.lblWhenWeatherChanges = new System.Windows.Forms.Label();
			this.comboIconChanges = new System.Windows.Forms.ComboBox();
			this.txtPostcode = new System.Windows.Forms.TextBox();
			this.lblPostCode = new System.Windows.Forms.Label();
			this.userControlTextLineIcon = new Mossywell.UKWeather.UserControlTextLine();
			this.userControlTextLineUpdating = new Mossywell.UKWeather.UserControlTextLine();
			this.userControlTextLinePostcode = new Mossywell.UKWeather.UserControlTextLine();
			((System.ComponentModel.ISupportInitialize)(this.trackIconFlashFrequency)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackUpdateInterval)).BeginInit();
			this.SuspendLayout();
			// 
			// lblUpdateInterval
			// 
			this.lblUpdateInterval.Location = new System.Drawing.Point(16, 88);
			this.lblUpdateInterval.Name = "lblUpdateInterval";
			this.lblUpdateInterval.Size = new System.Drawing.Size(144, 16);
			this.lblUpdateInterval.TabIndex = 5;
			this.lblUpdateInterval.Text = "Update interval";
			// 
			// trackIconFlashFrequency
			// 
			this.trackIconFlashFrequency.AutoSize = false;
			this.trackIconFlashFrequency.Location = new System.Drawing.Point(160, 168);
			this.trackIconFlashFrequency.Name = "trackIconFlashFrequency";
			this.trackIconFlashFrequency.Size = new System.Drawing.Size(80, 16);
			this.trackIconFlashFrequency.TabIndex = 0;
			this.trackIconFlashFrequency.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackIconFlashFrequency.Value = 10;
			this.trackIconFlashFrequency.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.trackIconFlashFrequency_HelpRequested);
			this.trackIconFlashFrequency.Scroll += new System.EventHandler(this.trackIconFlashFrequency_Scroll);
			// 
			// txtIconFlashFrequency
			// 
			this.txtIconFlashFrequency.Location = new System.Drawing.Point(248, 160);
			this.txtIconFlashFrequency.MaxLength = 4;
			this.txtIconFlashFrequency.Name = "txtIconFlashFrequency";
			this.txtIconFlashFrequency.Size = new System.Drawing.Size(64, 20);
			this.txtIconFlashFrequency.TabIndex = 4;
			this.txtIconFlashFrequency.Text = "";
			this.txtIconFlashFrequency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIconFlashFrequency_KeyPress);
			this.txtIconFlashFrequency.TextChanged += new System.EventHandler(this.txtIconFlashFrequency_TextChanged);
			this.txtIconFlashFrequency.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.txtIconFlashFrequency_HelpRequested);
			// 
			// lblIconFlashFrequency
			// 
			this.lblIconFlashFrequency.Location = new System.Drawing.Point(16, 168);
			this.lblIconFlashFrequency.Name = "lblIconFlashFrequency";
			this.lblIconFlashFrequency.Size = new System.Drawing.Size(144, 16);
			this.lblIconFlashFrequency.TabIndex = 0;
			this.lblIconFlashFrequency.Text = "Flash frequency";
			// 
			// txtUpdateInterval
			// 
			this.txtUpdateInterval.Location = new System.Drawing.Point(248, 80);
			this.txtUpdateInterval.MaxLength = 5;
			this.txtUpdateInterval.Name = "txtUpdateInterval";
			this.txtUpdateInterval.Size = new System.Drawing.Size(64, 20);
			this.txtUpdateInterval.TabIndex = 1;
			this.txtUpdateInterval.Tag = "";
			this.txtUpdateInterval.Text = "";
			this.txtUpdateInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUpdateInterval_KeyPress);
			this.txtUpdateInterval.TextChanged += new System.EventHandler(this.txtUpdateInterval_TextChanged);
			this.txtUpdateInterval.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.txtUpdateInterval_HelpRequested);
			// 
			// trackUpdateInterval
			// 
			this.trackUpdateInterval.AutoSize = false;
			this.trackUpdateInterval.Location = new System.Drawing.Point(160, 88);
			this.trackUpdateInterval.Name = "trackUpdateInterval";
			this.trackUpdateInterval.Size = new System.Drawing.Size(80, 16);
			this.trackUpdateInterval.TabIndex = 0;
			this.trackUpdateInterval.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackUpdateInterval.Value = 10;
			this.trackUpdateInterval.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.trackUpdateInterval_HelpRequested);
			this.trackUpdateInterval.Scroll += new System.EventHandler(this.trackUpdateInterval_Scroll);
			// 
			// lblWhenWeatherChanges
			// 
			this.lblWhenWeatherChanges.Location = new System.Drawing.Point(16, 144);
			this.lblWhenWeatherChanges.Name = "lblWhenWeatherChanges";
			this.lblWhenWeatherChanges.Size = new System.Drawing.Size(144, 16);
			this.lblWhenWeatherChanges.TabIndex = 0;
			this.lblWhenWeatherChanges.Text = "When the weather changes";
			// 
			// comboIconChanges
			// 
			this.comboIconChanges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboIconChanges.ItemHeight = 13;
			this.comboIconChanges.Location = new System.Drawing.Point(160, 136);
			this.comboIconChanges.Name = "comboIconChanges";
			this.comboIconChanges.Size = new System.Drawing.Size(152, 21);
			this.comboIconChanges.TabIndex = 3;
			this.comboIconChanges.SelectedIndexChanged += new System.EventHandler(this.comboIconChanges_SelectedIndexChanged);
			this.comboIconChanges.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.comboIconChanges_HelpRequested);
			// 
			// txtPostcode
			// 
			this.txtPostcode.Location = new System.Drawing.Point(248, 24);
			this.txtPostcode.MaxLength = 4;
			this.txtPostcode.Name = "txtPostcode";
			this.txtPostcode.Size = new System.Drawing.Size(64, 20);
			this.txtPostcode.TabIndex = 2;
			this.txtPostcode.Tag = "";
			this.txtPostcode.Text = "";
			this.txtPostcode.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.txtPostcode_HelpRequested);
			// 
			// lblPostCode
			// 
			this.lblPostCode.Location = new System.Drawing.Point(16, 32);
			this.lblPostCode.Name = "lblPostCode";
			this.lblPostCode.Size = new System.Drawing.Size(144, 16);
			this.lblPostCode.TabIndex = 0;
			this.lblPostCode.Text = "Postcode (first part only)";
			// 
			// userControlTextLineIcon
			// 
			this.userControlTextLineIcon.Location = new System.Drawing.Point(8, 120);
			this.userControlTextLineIcon.Name = "userControlTextLineIcon";
			this.userControlTextLineIcon.Size = new System.Drawing.Size(304, 16);
			this.userControlTextLineIcon.TabIndex = 9;
			this.userControlTextLineIcon.TextInLine = "Icon Settings";
			// 
			// userControlTextLineUpdating
			// 
			this.userControlTextLineUpdating.Location = new System.Drawing.Point(8, 64);
			this.userControlTextLineUpdating.Name = "userControlTextLineUpdating";
			this.userControlTextLineUpdating.Size = new System.Drawing.Size(304, 16);
			this.userControlTextLineUpdating.TabIndex = 7;
			this.userControlTextLineUpdating.TextInLine = "Update Settings";
			// 
			// userControlTextLinePostcode
			// 
			this.userControlTextLinePostcode.Location = new System.Drawing.Point(8, 8);
			this.userControlTextLinePostcode.Name = "userControlTextLinePostcode";
			this.userControlTextLinePostcode.Size = new System.Drawing.Size(304, 16);
			this.userControlTextLinePostcode.TabIndex = 6;
			this.userControlTextLinePostcode.TextInLine = "Postcode Settings";
			// 
			// OptionsControlUpdating
			// 
			this.Controls.Add(this.lblUpdateInterval);
			this.Controls.Add(this.trackIconFlashFrequency);
			this.Controls.Add(this.txtIconFlashFrequency);
			this.Controls.Add(this.lblIconFlashFrequency);
			this.Controls.Add(this.txtUpdateInterval);
			this.Controls.Add(this.trackUpdateInterval);
			this.Controls.Add(this.lblWhenWeatherChanges);
			this.Controls.Add(this.comboIconChanges);
			this.Controls.Add(this.txtPostcode);
			this.Controls.Add(this.lblPostCode);
			this.Controls.Add(this.userControlTextLinePostcode);
			this.Controls.Add(this.userControlTextLineUpdating);
			this.Controls.Add(this.userControlTextLineIcon);
			this.Name = "OptionsControlUpdating";
			this.Size = new System.Drawing.Size(320, 192);
			((System.ComponentModel.ISupportInitialize)(this.trackIconFlashFrequency)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackUpdateInterval)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Utility Methods
		internal void IconFlashFrequencyStatus()
		{
			// Only allow the icon flash frequency box if the option is set to flash or flash continuously
			if(comboIconChanges.SelectedItem.ToString() != Utils.GetEnumDescription(IconChangeOptions.Flash) &&
				comboIconChanges.SelectedItem.ToString() != Utils.GetEnumDescription(IconChangeOptions.FlashContinuously))
			{
				lblIconFlashFrequency.Enabled   = false;
				txtIconFlashFrequency.Enabled   = false;
				trackIconFlashFrequency.Enabled = false;
			}
			else
			{
				lblIconFlashFrequency.Enabled   = true;
				txtIconFlashFrequency.Enabled   = true;
				trackIconFlashFrequency.Enabled = true;
			}
		}
		
		private void MatchTrackBarToText(TextBox textbox, TrackBar trackbar)
		{
			int val;
					
			if(textbox.Text == "")
				val = 0;
			else
				val = Convert.ToInt32(textbox.Text); // KeyPress event ensures numbers only

			if(val < trackbar.Minimum)
				trackbar.Value = trackbar.Minimum;
			else if(val > trackbar.Maximum)
				trackbar.Value = trackbar.Maximum;
			else
				trackbar.Value = val;
		}

		private void MatchTextToTrackBar(TrackBar trackbar, TextBox textbox)
		{
			textbox.Text = trackbar.Value.ToString();
		}

		private void CheckNumbersOnly(System.Windows.Forms.KeyPressEventArgs e)
		{
			int intKeyCode = (int)e.KeyChar;

			if((intKeyCode < 48 &&  intKeyCode != 8) || intKeyCode > 57)
			{
				e.Handled = true;
			}
		}
		#endregion

		#region Events
		private void comboIconChanges_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			IconFlashFrequencyStatus();
		}

		private void trackUpdateInterval_Scroll(object sender, System.EventArgs e)
		{
			MatchTextToTrackBar(trackUpdateInterval, txtUpdateInterval);
		}

		private void txtUpdateInterval_TextChanged(object sender, System.EventArgs e)
		{
			MatchTrackBarToText(txtUpdateInterval, trackUpdateInterval);
		}

		private void txtUpdateInterval_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			// Maxlength of this field is 5 chars (99999)
			CheckNumbersOnly(e);
		}

		private void trackIconFlashFrequency_Scroll(object sender, System.EventArgs e)
		{
			MatchTextToTrackBar(trackIconFlashFrequency, txtIconFlashFrequency);		
		}

		private void txtIconFlashFrequency_TextChanged(object sender, System.EventArgs e)
		{
			MatchTrackBarToText(txtIconFlashFrequency, trackIconFlashFrequency);
		}

		private void txtIconFlashFrequency_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			// Maxlength of this field is 4 chars (9999)
			CheckNumbersOnly(e);
		}

		private void trackUpdateInterval_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsUpdateInterval);
		}

		private void txtUpdateInterval_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsUpdateInterval);			
		}

		private void txtPostcode_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsPostcode);					
		}

		private void comboIconChanges_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsWhenChanges);								
		}

		private void txtIconFlashFrequency_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsFlashFrequency);
		}

		private void trackIconFlashFrequency_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsFlashFrequency);
		}
	  #endregion
	}
}
