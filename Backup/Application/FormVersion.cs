using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	public class FormVersion : System.Windows.Forms.Form
	{
		#region Class Fields
		private System.Windows.Forms.Label lblThisVersionText;
		private System.Windows.Forms.Label lblLatestVersionText;
		private System.Windows.Forms.Label lblThisVersion;
		private System.Windows.Forms.Label lblLatestVersion;
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnNo;
		private System.ComponentModel.IContainer components = null;

		private string _strCurrentVersion;
		private System.Windows.Forms.CheckBox chkDoVersionCheck;
		private Mossywell.UKWeather.UserControlTextLine userControlTextLine1;
		private string _strLatestVersion;
		#endregion

		#region Constructor
		public FormVersion(string currentversion, string latestversion)
		{
			InitializeComponent();

			_strCurrentVersion = currentversion;
			_strLatestVersion  = latestversion;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormVersion));
			this.lblThisVersionText = new System.Windows.Forms.Label();
			this.lblLatestVersionText = new System.Windows.Forms.Label();
			this.lblThisVersion = new System.Windows.Forms.Label();
			this.lblLatestVersion = new System.Windows.Forms.Label();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.chkDoVersionCheck = new System.Windows.Forms.CheckBox();
			this.userControlTextLine1 = new Mossywell.UKWeather.UserControlTextLine();
			this.SuspendLayout();
			// 
			// lblThisVersionText
			// 
			this.lblThisVersionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblThisVersionText.Location = new System.Drawing.Point(8, 8);
			this.lblThisVersionText.Name = "lblThisVersionText";
			this.lblThisVersionText.Size = new System.Drawing.Size(192, 16);
			this.lblThisVersionText.TabIndex = 0;
			this.lblThisVersionText.Text = "This version of UK Weather is";
			this.lblThisVersionText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLatestVersionText
			// 
			this.lblLatestVersionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblLatestVersionText.Location = new System.Drawing.Point(8, 24);
			this.lblLatestVersionText.Name = "lblLatestVersionText";
			this.lblLatestVersionText.Size = new System.Drawing.Size(192, 16);
			this.lblLatestVersionText.TabIndex = 1;
			this.lblLatestVersionText.Text = "The latest version of UK Weather is";
			this.lblLatestVersionText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblThisVersion
			// 
			this.lblThisVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblThisVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblThisVersion.Location = new System.Drawing.Point(200, 8);
			this.lblThisVersion.Name = "lblThisVersion";
			this.lblThisVersion.Size = new System.Drawing.Size(136, 16);
			this.lblThisVersion.TabIndex = 2;
			this.lblThisVersion.Text = "?";
			this.lblThisVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLatestVersion
			// 
			this.lblLatestVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLatestVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lblLatestVersion.Location = new System.Drawing.Point(200, 24);
			this.lblLatestVersion.Name = "lblLatestVersion";
			this.lblLatestVersion.Size = new System.Drawing.Size(136, 16);
			this.lblLatestVersion.TabIndex = 3;
			this.lblLatestVersion.Text = "?";
			this.lblLatestVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnYes
			// 
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnYes.Location = new System.Drawing.Point(8, 72);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(80, 24);
			this.btnYes.TabIndex = 5;
			this.btnYes.Text = "Download";
			this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
			// 
			// btnNo
			// 
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNo.Location = new System.Drawing.Point(112, 72);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(80, 24);
			this.btnNo.TabIndex = 6;
			this.btnNo.Text = "Ignore";
			this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
			// 
			// chkDoVersionCheck
			// 
			this.chkDoVersionCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkDoVersionCheck.Location = new System.Drawing.Point(224, 80);
			this.chkDoVersionCheck.Name = "chkDoVersionCheck";
			this.chkDoVersionCheck.Size = new System.Drawing.Size(112, 16);
			this.chkDoVersionCheck.TabIndex = 7;
			this.chkDoVersionCheck.Text = "Don\'t ask me again";
			// 
			// userControlTextLine1
			// 
			this.userControlTextLine1.Location = new System.Drawing.Point(8, 56);
			this.userControlTextLine1.Name = "userControlTextLine1";
			this.userControlTextLine1.Size = new System.Drawing.Size(328, 11);
			this.userControlTextLine1.TabIndex = 8;
			this.userControlTextLine1.TextInLine = "";
			// 
			// FormVersion
			// 
			this.AcceptButton = this.btnYes;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnNo;
			this.ClientSize = new System.Drawing.Size(346, 104);
			this.Controls.Add(this.userControlTextLine1);
			this.Controls.Add(this.chkDoVersionCheck);
			this.Controls.Add(this.btnNo);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.lblLatestVersion);
			this.Controls.Add(this.lblThisVersion);
			this.Controls.Add(this.lblLatestVersionText);
			this.Controls.Add(this.lblThisVersionText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormVersion";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "UK Weather New Version Available";
			this.Load += new System.EventHandler(this.FormVersion_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void FormVersion_Load(object sender, System.EventArgs e)
		{
			this.lblThisVersion.Text   = _strCurrentVersion;
			this.lblLatestVersion.Text = _strLatestVersion;
		}

		private void btnYes_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnNo_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		#endregion

		#region Properties
		internal string DoVersionCheck
		{
			get
			{
				if(this.chkDoVersionCheck.Checked)
					return "0";
				else
					return "1";
			}
		}
		#endregion
	}
}
