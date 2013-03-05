using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	public class FormStatus : System.Windows.Forms.Form
	{
		#region Class Fields
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Timer timerProgress;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.ComponentModel.IContainer components;
		#endregion

		#region Constructor
		public FormStatus()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormStatus));
			this.lblStatus = new System.Windows.Forms.Label();
			this.timerProgress = new System.Windows.Forms.Timer(this.components);
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// lblStatus
			// 
			this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.lblStatus.Location = new System.Drawing.Point(16, 40);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(280, 32);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timerProgress
			// 
			this.timerProgress.Enabled = true;
			this.timerProgress.Tick += new System.EventHandler(this.timerProgress_Tick);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(8, 8);
			this.progressBar.Maximum = 50;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(288, 23);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 1;
			// 
			// FormStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(306, 80);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.lblStatus);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormStatus";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Version and data URL check...";
			this.ResumeLayout(false);

		}
		#endregion

		#region Utility Methods
		public void AddText(string message)
		{
			lblStatus.Text += message;
			lblStatus.Update();
		}

		public void FillProgressBar()
		{
			progressBar.Value = progressBar.Maximum;
			progressBar.Update();
		}

		public void Stop()
		{
			this.timerProgress.Stop();
		}
    #endregion

		#region Events

		private void timerProgress_Tick(object sender, System.EventArgs e)
		{
			this.progressBar.Increment(1);
		}
	  #endregion
	}
}
