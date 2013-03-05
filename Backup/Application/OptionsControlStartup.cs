using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	public class OptionsControlStartup : UserControl
	{
		#region Class Fields
		// Note that we make some internal rather than using properties
		// as it's much faster and easier to code! Purists might complain...
		private System.Windows.Forms.Label lblRunAtStartup;
		internal System.Windows.Forms.CheckBox chkRunAtStartup;
		private System.Windows.Forms.Label lblNewVersionPopup;
		internal System.Windows.Forms.CheckBox chkNewVersionPopup;
		private System.Windows.Forms.Label lblCheckVersionAndUrl;
		internal System.Windows.Forms.CheckBox chkCheckVersionAndUrl;
		private Mossywell.UKWeather.UserControlTextLine userControlTextLine1;
		private System.ComponentModel.IContainer components = null;
		#endregion

		#region Constructor
		public OptionsControlStartup()
		{
			InitializeComponent();
		}
		#endregion

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblRunAtStartup = new System.Windows.Forms.Label();
			this.chkRunAtStartup = new System.Windows.Forms.CheckBox();
			this.lblNewVersionPopup = new System.Windows.Forms.Label();
			this.chkNewVersionPopup = new System.Windows.Forms.CheckBox();
			this.lblCheckVersionAndUrl = new System.Windows.Forms.Label();
			this.chkCheckVersionAndUrl = new System.Windows.Forms.CheckBox();
			this.userControlTextLine1 = new Mossywell.UKWeather.UserControlTextLine();
			this.SuspendLayout();
			// 
			// lblRunAtStartup
			// 
			this.lblRunAtStartup.Location = new System.Drawing.Point(16, 32);
			this.lblRunAtStartup.Name = "lblRunAtStartup";
			this.lblRunAtStartup.Size = new System.Drawing.Size(232, 16);
			this.lblRunAtStartup.TabIndex = 3;
			this.lblRunAtStartup.Text = "Run at system startup";
			// 
			// chkRunAtStartup
			// 
			this.chkRunAtStartup.Location = new System.Drawing.Point(296, 32);
			this.chkRunAtStartup.Name = "chkRunAtStartup";
			this.chkRunAtStartup.Size = new System.Drawing.Size(16, 16);
			this.chkRunAtStartup.TabIndex = 4;
			this.chkRunAtStartup.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.chkRunAtStartup_HelpRequested);
			// 
			// lblNewVersionPopup
			// 
			this.lblNewVersionPopup.Location = new System.Drawing.Point(16, 48);
			this.lblNewVersionPopup.Name = "lblNewVersionPopup";
			this.lblNewVersionPopup.Size = new System.Drawing.Size(232, 16);
			this.lblNewVersionPopup.TabIndex = 5;
			this.lblNewVersionPopup.Text = "Notify user if new version is found";
			// 
			// chkNewVersionPopup
			// 
			this.chkNewVersionPopup.Location = new System.Drawing.Point(296, 48);
			this.chkNewVersionPopup.Name = "chkNewVersionPopup";
			this.chkNewVersionPopup.Size = new System.Drawing.Size(16, 16);
			this.chkNewVersionPopup.TabIndex = 6;
			this.chkNewVersionPopup.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.chkNewVersionPopup_HelpRequested);
			// 
			// lblCheckVersionAndUrl
			// 
			this.lblCheckVersionAndUrl.Location = new System.Drawing.Point(16, 64);
			this.lblCheckVersionAndUrl.Name = "lblCheckVersionAndUrl";
			this.lblCheckVersionAndUrl.Size = new System.Drawing.Size(232, 16);
			this.lblCheckVersionAndUrl.TabIndex = 7;
			this.lblCheckVersionAndUrl.Text = "Check for new version and URL";
			// 
			// chkCheckVersionAndUrl
			// 
			this.chkCheckVersionAndUrl.Location = new System.Drawing.Point(296, 64);
			this.chkCheckVersionAndUrl.Name = "chkCheckVersionAndUrl";
			this.chkCheckVersionAndUrl.Size = new System.Drawing.Size(16, 16);
			this.chkCheckVersionAndUrl.TabIndex = 8;
			this.chkCheckVersionAndUrl.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.chkCheckVersionAndUrl_HelpRequested);
			// 
			// userControlTextLine1
			// 
			this.userControlTextLine1.Location = new System.Drawing.Point(8, 8);
			this.userControlTextLine1.Name = "userControlTextLine1";
			this.userControlTextLine1.Size = new System.Drawing.Size(304, 16);
			this.userControlTextLine1.TabIndex = 9;
			this.userControlTextLine1.TextInLine = "Startup Settings";
			// 
			// OptionsControlStartup
			// 
			this.Controls.Add(this.userControlTextLine1);
			this.Controls.Add(this.chkCheckVersionAndUrl);
			this.Controls.Add(this.lblCheckVersionAndUrl);
			this.Controls.Add(this.chkNewVersionPopup);
			this.Controls.Add(this.lblNewVersionPopup);
			this.Controls.Add(this.chkRunAtStartup);
			this.Controls.Add(this.lblRunAtStartup);
			this.Name = "OptionsControlStartup";
			this.Size = new System.Drawing.Size(320, 88);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void chkRunAtStartup_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsStartup);										
		}

		private void chkNewVersionPopup_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsNotifyUser);
		}

		private void chkCheckVersionAndUrl_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.OptionsCheckVerAndUrl);
		}
		#endregion
	}
}

