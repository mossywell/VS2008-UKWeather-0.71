using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices; // DllImport
using System.Reflection;              // Assembly
using System.IO;                      // Stream

namespace Mossywell
{
	namespace UKWeather
	{
		internal enum SND
		{
			SND_SYNC = 0x0000 ,        /* play synchronously (default) */
			SND_ASYNC = 0x0001 ,       /* play asynchronously */
			SND_NODEFAULT = 0x0002 ,   /* silence (!default) if sound not found */
			SND_MEMORY = 0x0004 ,      /* pszSound points to a memory file */
			SND_LOOP = 0x0008 ,        /* loop the sound until next sndPlaySound */
			SND_NOSTOP = 0x0010 ,      /* don't stop any currently playing sound */
			SND_NOWAIT = 0x00002000,   /* don't wait if the driver is busy */
			SND_ALIAS = 0x00010000 ,   /* name is a registry alias */
			SND_ALIAS_ID = 0x00110000, /* alias is a pre d ID */
			SND_FILENAME = 0x00020000, /* name is file name */
			SND_RESOURCE = 0x00040004, /* name is resource name or atom */
			SND_PURGE = 0x0040,        /* purge non-static events for task */
			SND_APPLICATION = 0x0080   /* look for application specific association */
		}

		internal class FormAbout : System.Windows.Forms.Form
		{
			#region Externals
			[DllImport("winmm.dll")]
			internal static extern long mciSendString(string lpstrCommand, string lpstrReturnString, ulong uReturnLength, ulong hwndCallback);
			#endregion

			#region Class Structures
			internal struct Lyrics
			{
				internal int SleepPeriod;
				internal string Text;
			}	
			#endregion

			#region Class Fields
			private bool     _blnShift              = false;
			private bool     _blnControl            = false;
			private bool     _blnAlt                = false;
			private bool     _blnSpace              = false;
			private string   _strFile               = "~tmp.wma";
			private string   _strUrl;
			private FileInfo _fi                    = null;
			private System.Windows.Forms.Button btnOK;
			private System.Windows.Forms.Label lblTitle;
			private System.Windows.Forms.RichTextBox rtxtBox;
			private System.Windows.Forms.PictureBox pictureBox;
			private System.Windows.Forms.Label lblVersion;
			private System.Windows.Forms.TextBox txtVersion;
			private System.Windows.Forms.Label lblLocation;
			private System.Windows.Forms.TextBox txtLocation;
			private System.Windows.Forms.Label lblUrl;
			private System.Windows.Forms.TextBox txtUrl;
			private System.ComponentModel.Container components = null;
			#endregion

			#region Constructor
			internal FormAbout(string url)
			{
				InitializeComponent();
				_strUrl = url;
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
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormAbout));
				this.btnOK = new System.Windows.Forms.Button();
				this.lblTitle = new System.Windows.Forms.Label();
				this.rtxtBox = new System.Windows.Forms.RichTextBox();
				this.pictureBox = new System.Windows.Forms.PictureBox();
				this.txtVersion = new System.Windows.Forms.TextBox();
				this.lblVersion = new System.Windows.Forms.Label();
				this.lblLocation = new System.Windows.Forms.Label();
				this.txtLocation = new System.Windows.Forms.TextBox();
				this.lblUrl = new System.Windows.Forms.Label();
				this.txtUrl = new System.Windows.Forms.TextBox();
				this.SuspendLayout();
				// 
				// btnOK
				// 
				this.btnOK.BackColor = System.Drawing.Color.White;
				this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
				this.btnOK.ForeColor = System.Drawing.Color.MidnightBlue;
				this.btnOK.Location = new System.Drawing.Point(256, 144);
				this.btnOK.Name = "btnOK";
				this.btnOK.TabIndex = 1;
				this.btnOK.Text = "OK";
				this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
				// 
				// lblTitle
				// 
				this.lblTitle.BackColor = System.Drawing.Color.Transparent;
				this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
				this.lblTitle.Location = new System.Drawing.Point(109, 8);
				this.lblTitle.Name = "lblTitle";
				this.lblTitle.Size = new System.Drawing.Size(376, 40);
				this.lblTitle.TabIndex = 3;
				this.lblTitle.Text = "Mossywell\'s UK Weather";
				this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
				// 
				// rtxtBox
				// 
				this.rtxtBox.BackColor = System.Drawing.Color.White;
				this.rtxtBox.ForeColor = System.Drawing.Color.DarkRed;
				this.rtxtBox.Location = new System.Drawing.Point(8, 120);
				this.rtxtBox.Name = "rtxtBox";
				this.rtxtBox.ReadOnly = true;
				this.rtxtBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
				this.rtxtBox.Size = new System.Drawing.Size(232, 72);
				this.rtxtBox.TabIndex = 6;
				this.rtxtBox.TabStop = false;
				this.rtxtBox.Text = "";
				this.rtxtBox.Visible = false;
				// 
				// pictureBox
				// 
				this.pictureBox.BackColor = System.Drawing.Color.Transparent;
				this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
				this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
				this.pictureBox.Location = new System.Drawing.Point(8, 8);
				this.pictureBox.Name = "pictureBox";
				this.pictureBox.Size = new System.Drawing.Size(48, 48);
				this.pictureBox.TabIndex = 7;
				this.pictureBox.TabStop = false;
				this.pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
				// 
				// txtVersion
				// 
				this.txtVersion.BackColor = System.Drawing.SystemColors.ControlLight;
				this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
				this.txtVersion.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.txtVersion.Location = new System.Drawing.Point(240, 52);
				this.txtVersion.Name = "txtVersion";
				this.txtVersion.ReadOnly = true;
				this.txtVersion.Size = new System.Drawing.Size(352, 13);
				this.txtVersion.TabIndex = 8;
				this.txtVersion.Text = "";
				// 
				// lblVersion
				// 
				this.lblVersion.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.lblVersion.Location = new System.Drawing.Point(96, 52);
				this.lblVersion.Name = "lblVersion";
				this.lblVersion.Size = new System.Drawing.Size(136, 16);
				this.lblVersion.TabIndex = 9;
				this.lblVersion.Text = "Version: ";
				this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				// 
				// lblLocation
				// 
				this.lblLocation.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.lblLocation.Location = new System.Drawing.Point(96, 72);
				this.lblLocation.Name = "lblLocation";
				this.lblLocation.Size = new System.Drawing.Size(136, 16);
				this.lblLocation.TabIndex = 10;
				this.lblLocation.Text = "Executable Location:";
				this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
				// 
				// txtLocation
				// 
				this.txtLocation.BackColor = System.Drawing.SystemColors.ControlLight;
				this.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
				this.txtLocation.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.txtLocation.Location = new System.Drawing.Point(240, 72);
				this.txtLocation.Name = "txtLocation";
				this.txtLocation.ReadOnly = true;
				this.txtLocation.Size = new System.Drawing.Size(352, 13);
				this.txtLocation.TabIndex = 11;
				this.txtLocation.Text = "";
				// 
				// lblUrl
				// 
				this.lblUrl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.lblUrl.Location = new System.Drawing.Point(96, 92);
				this.lblUrl.Name = "lblUrl";
				this.lblUrl.Size = new System.Drawing.Size(136, 16);
				this.lblUrl.TabIndex = 12;
				this.lblUrl.Text = "Data URL:";
				// 
				// txtUrl
				// 
				this.txtUrl.BackColor = System.Drawing.SystemColors.ControlLight;
				this.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
				this.txtUrl.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.txtUrl.Location = new System.Drawing.Point(240, 92);
				this.txtUrl.Name = "txtUrl";
				this.txtUrl.ReadOnly = true;
				this.txtUrl.Size = new System.Drawing.Size(352, 13);
				this.txtUrl.TabIndex = 13;
				this.txtUrl.Text = "";
				// 
				// FormAbout
				// 
				this.AcceptButton = this.btnOK;
                this.AutoScaleMode = AutoScaleMode.Inherit;
				this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
				this.BackColor = System.Drawing.SystemColors.ControlLight;
				this.ClientSize = new System.Drawing.Size(594, 199);
				this.Controls.Add(this.txtUrl);
				this.Controls.Add(this.txtLocation);
				this.Controls.Add(this.txtVersion);
				this.Controls.Add(this.lblUrl);
				this.Controls.Add(this.lblLocation);
				this.Controls.Add(this.lblVersion);
				this.Controls.Add(this.pictureBox);
				this.Controls.Add(this.rtxtBox);
				this.Controls.Add(this.lblTitle);
				this.Controls.Add(this.btnOK);
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
				this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
				this.KeyPreview = true;
				this.MaximizeBox = false;
				this.MinimizeBox = false;
				this.Name = "FormAbout";
				this.ShowInTaskbar = false;
				this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
				this.Tag = "";
				this.Text = "About...";
				this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAbout_KeyDown);
				this.Closing += new System.ComponentModel.CancelEventHandler(this.FormAbout_Closing);
				this.Load += new System.EventHandler(this.FormAbout_Load);
				this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FormAbout_HelpRequested);
				this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormAbout_KeyUp);
				this.ResumeLayout(false);

			}
			#endregion

			#region Events
			private void FormAbout_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
			{
				Utils.GetHelp(this, hlpevent, HelpFile.FormAbout);
			}

			private void btnOK_Click(object sender, System.EventArgs e)
			{
				this.Close();
			}

			private void FormAbout_Load(object sender, System.EventArgs e)
			{
				Version ver = new Version(ProductVersion);
				txtVersion.Text = ver.ToString();
				txtLocation.Text = Utils.GetExecutableLocation();
				this.txtUrl.Text = _strUrl;
			}

			private void pictureBox_DoubleClick(object sender, System.EventArgs e)
			{
				Stream     s;
				byte[]     ba;
				FileStream fs     = null;
				Lyrics[]   lyrics = new Lyrics[5];

				if(_blnShift)
				{
					s       = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.UKWeather.Unwinese.wma");
					lyrics[0].SleepPeriod = 2750;
					lyrics[0].Text        = "Lift that barge," + Environment.NewLine;
					lyrics[1].SleepPeriod = 830;
					lyrics[1].Text        = "Gruertin in the foil," + Environment.NewLine;
					lyrics[2].SleepPeriod = 830;
					lyrics[2].Text        = "Stuffin' o' the ship," + Environment.NewLine;
					lyrics[3].SleepPeriod = 830;
					lyrics[3].Text        = "And all come boil!";
					lyrics[4].SleepPeriod = 0;
					lyrics[4].Text        = "";
				}
				else if(_blnControl)
				{
					s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.UKWeather.Orion.wma");
					lyrics[0].SleepPeriod = 50;
					lyrics[0].Text        = "I always think of Orion as the" + Environment.NewLine;
					lyrics[1].SleepPeriod = 1400;
					lyrics[1].Text        = "loveliest constellation in the entire sky," + Environment.NewLine;
					lyrics[2].SleepPeriod = 2000;
					lyrics[2].Text        = "so, woob thing thee thears.";
					lyrics[3].SleepPeriod = 0;
					lyrics[3].Text        = "";
					lyrics[4].SleepPeriod = 0;
					lyrics[4].Text        = "";
				}
				else if(_blnAlt)
				{
					s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.UKWeather.FH.wma");
					lyrics[0].SleepPeriod = 50;
					lyrics[0].Text        = "Fuckin' 'ell!";
					lyrics[1].SleepPeriod = 0;
					lyrics[1].Text        = "";
					lyrics[2].SleepPeriod = 0;
					lyrics[2].Text        = "";
					lyrics[3].SleepPeriod = 0;
					lyrics[3].Text        = "";
					lyrics[4].SleepPeriod = 0;
					lyrics[4].Text        = "";
				}
				else if(_blnSpace)
				{
					s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Mossywell.UKWeather.Lezby.wma");
					lyrics[0].SleepPeriod = 50;
					lyrics[0].Text        = "A message..." + Environment.NewLine;
					lyrics[1].SleepPeriod = 1470;
					lyrics[1].Text        = "for the bezz fozzbowl serports in the world," + Environment.NewLine;
					lyrics[2].SleepPeriod = 4110;
					lyrics[2].Text        = "wee eeea twelf man here." + Environment.NewLine;
					lyrics[3].SleepPeriod = 2100;
					lyrics[3].Text        = "Where are you? WHERE ARE YOU?" + Environment.NewLine;
					lyrics[4].SleepPeriod = 5000;
					lyrics[4].Text        = "Lezby Avenue! Comeooon! [hic].";
				}
				else
				{
					s = null;
					return;
				}

				// Just in case one's still playing, blast it...
				StopPlay();

				// Read the stream into the byte array
				ba = new byte[s.Length];
				s.Read(ba, 0, (int)s.Length);

				// Send it back to the output file
				_fi = new FileInfo(_strFile);
				try
				{
					fs  = _fi.OpenWrite();
				}
				catch
				{
					// Not given the file system enough time to delete the file probably
					System.Threading.Thread.Sleep(100);
					fs = _fi.OpenWrite();
				}
				fs.Write(ba, 0, (int)s.Length);
				fs.Close();
				fs  = null;

				// Play the file then close it
				mciSendString("play " + _strFile, null, 0, 0);

				rtxtBox.Text = "";
				rtxtBox.Update();
				for(int i = 0; i < lyrics.Length; i++)
				{
					if(lyrics[i].Text != "")
					{
						this.AddToTextBox(lyrics[i].SleepPeriod, lyrics[i].Text);
					}
				}
				_fi.Delete();
				_fi = null;
			}

  		private void FormAbout_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
			{
				if(e.KeyCode == Keys.ControlKey)
					_blnControl = true;
				if(e.KeyCode == Keys.ShiftKey)
					_blnShift   = true;
				if(e.KeyCode == Keys.Menu)
					_blnAlt     = true;
				if(e.KeyCode == Keys.Space)
				{
					_blnSpace   = true;
					e.Handled   = true; // Throw away the system interpretation of space bar
				}
			}

			private void FormAbout_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
			{
				if(e.KeyCode == Keys.ControlKey)
					_blnControl = false;
				if(e.KeyCode == Keys.ShiftKey)
					_blnShift   = false;
				if(e.KeyCode == Keys.Menu)
					_blnAlt     = false;
				if(e.KeyCode == Keys.Space)
					_blnSpace   = false;
			}

			private void FormAbout_Closing(object sender, System.ComponentModel.CancelEventArgs e)
			{
				// Stop the sound
				StopPlay();
			}
			#endregion

			#region Utility Methods
			private void AddToTextBox(int sleep, string message)
			{
				System.Threading.Thread.Sleep(sleep);
				rtxtBox.Text   += message;
				rtxtBox.Visible = true;
				rtxtBox.Update();
			}

			private void StopPlay()
			{
				mciSendString("stop " + _strFile, null, 0, 0);
				try   { _fi.Delete(); }
				catch {}
				_fi = null;
			}
			#endregion

		}
	}
}
