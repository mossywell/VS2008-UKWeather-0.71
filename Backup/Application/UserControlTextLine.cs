using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Mossywell.UKWeather
{
	public class UserControlTextLine : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox;
		private System.ComponentModel.Container components = null;
		public string MYString;

		public UserControlTextLine()
		{
			InitializeComponent();
		}

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Location = new System.Drawing.Point(-9, 0);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(737, 24);
			this.groupBox.TabIndex = 0;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "?";
			// 
			// UserControlTextLine
			// 
			this.Controls.Add(this.groupBox);
			this.Name = "UserControlTextLine";
			this.Size = new System.Drawing.Size(720, 11);
			this.ResumeLayout(false);

		}
		#endregion

		public string TextInLine
		{
			get
			{
				return groupBox.Text;
			}
			set
			{
				groupBox.Text = value;
			}
		}
	}
}
