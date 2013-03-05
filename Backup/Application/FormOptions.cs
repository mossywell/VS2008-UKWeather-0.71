using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;        // RegistryKey

namespace Mossywell.UKWeather
{
	internal class FormOptions : System.Windows.Forms.Form
	{
		#region Class Fields
		private OptionsControlUi _optionsControlUi;
		private OptionsControlUpdating _optionsControlUpdating;
		private OptionsControlStartup _optionsControlStartup;
		private UserOptions _userOptions;

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOKAndUpdate;
		private System.Windows.Forms.Panel panelOptions;
		private System.Windows.Forms.TreeView treeOptions;
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructor
		internal FormOptions(UserOptions useroptions)
		{
			InitializeComponent();
		
			_userOptions  = useroptions;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormOptions));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOKAndUpdate = new System.Windows.Forms.Button();
			this.panelOptions = new System.Windows.Forms.Panel();
			this.treeOptions = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(220, 224);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 24);
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(336, 224);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 24);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOKAndUpdate
			// 
			this.btnOKAndUpdate.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnOKAndUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOKAndUpdate.Location = new System.Drawing.Point(104, 224);
			this.btnOKAndUpdate.Name = "btnOKAndUpdate";
			this.btnOKAndUpdate.Size = new System.Drawing.Size(88, 24);
			this.btnOKAndUpdate.TabIndex = 6;
			this.btnOKAndUpdate.Text = "OK and Update";
			this.btnOKAndUpdate.Click += new System.EventHandler(this.btnOKAndUpdate_Click);
			// 
			// panelOptions
			// 
			this.panelOptions.Location = new System.Drawing.Point(104, 8);
			this.panelOptions.Name = "panelOptions";
			this.panelOptions.Size = new System.Drawing.Size(320, 192);
			this.panelOptions.TabIndex = 9;
			// 
			// treeOptions
			// 
			this.treeOptions.ImageIndex = -1;
			this.treeOptions.Location = new System.Drawing.Point(8, 8);
			this.treeOptions.Name = "treeOptions";
			this.treeOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																																						new System.Windows.Forms.TreeNode("User Interface"),
																																						new System.Windows.Forms.TreeNode("Updating"),
																																						new System.Windows.Forms.TreeNode("Startup")});
			this.treeOptions.Scrollable = false;
			this.treeOptions.SelectedImageIndex = -1;
			this.treeOptions.ShowPlusMinus = false;
			this.treeOptions.ShowRootLines = false;
			this.treeOptions.Size = new System.Drawing.Size(88, 240);
			this.treeOptions.TabIndex = 10;
			this.treeOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeOptions_AfterSelect);
			this.treeOptions.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeOptions_BeforeCollapse);
			// 
			// FormOptions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(434, 255);
			this.Controls.Add(this.treeOptions);
			this.Controls.Add(this.panelOptions);
			this.Controls.Add(this.btnOKAndUpdate);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormOptions";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Tag = "running-options.htm";
			this.Text = "UK Weather Options";
			this.Load += new System.EventHandler(this.FormOptions_Load);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FormOptions_HelpRequested);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void FormOptions_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
			Utils.GetHelp(this, hlpevent, HelpFile.FormOptions);
		}

		private void FormOptions_Load(object sender, System.EventArgs e)
		{
			// Instantiate the options usercontrols...
			_optionsControlUi = new OptionsControlUi();
			_optionsControlUpdating = new OptionsControlUpdating();
			_optionsControlStartup = new OptionsControlStartup();
			

			// Expand the tree view
			treeOptions.ExpandAll();
	
			// Check version and url?
			_optionsControlStartup.chkCheckVersionAndUrl.Checked = _userOptions.CheckVersionAndUrl;

			// Icon changes options
			IconChangeOptions ico;
			for(ico = IconChangeOptions.FirstValue; ico < IconChangeOptions.LastValue; ico++)
			{
				if(ico != IconChangeOptions.FirstValue && ico != IconChangeOptions.LastValue)
				{			
					_optionsControlUpdating.comboIconChanges.Items.Add(Utils.GetEnumDescription(ico));
				}
			}
			_optionsControlUpdating.comboIconChanges.SelectedItem = Utils.GetEnumDescription(_userOptions.IconChangeOption);

			// Temperature Scale
			switch(_userOptions.TemperatureScale)
			{
				case TemperatureScales.Farenheit:
					_optionsControlUi.radioCelsius.Checked = false;
					_optionsControlUi.radioFarenheit.Checked = true;
					break;
				case TemperatureScales.Celsius:
				default:
					_optionsControlUi.radioCelsius.Checked = true;
					_optionsControlUi.radioFarenheit.Checked = false;
					break;
			}

			// Icon Flash Frequency - MUST COME AFTER SET ICON CHANGES OPTIONS
			_optionsControlUpdating.trackIconFlashFrequency.Minimum = Constants.MIN_ICONFLASHFREQUENCY;
			_optionsControlUpdating.trackIconFlashFrequency.Maximum = Constants.MAX_ICONFLASHFREQUENCY;
			_optionsControlUpdating.txtIconFlashFrequency.Text      = Convert.ToString(_userOptions.IconFlashFrequency);
			_optionsControlUpdating.trackIconFlashFrequency.Value   = _userOptions.IconFlashFrequency;
			_optionsControlUpdating.IconFlashFrequencyStatus();

			// New version popup?
			_optionsControlStartup.chkNewVersionPopup.Checked = _userOptions.NewVersionPopup;

			// Postcode
			_optionsControlUpdating.txtPostcode.Text            = _userOptions.Postcode;

			// Are we set to run at startup?
			_optionsControlStartup.chkRunAtStartup.Checked = _userOptions.RunAtSystemStartup;

			// Update interval
			_optionsControlUpdating.trackUpdateInterval.Minimum = Constants.MIN_UPDATEINTERVAL;
			_optionsControlUpdating.trackUpdateInterval.Maximum = Constants.MAX_UPDATEINTERVAL;
			_optionsControlUpdating.txtUpdateInterval.Text      = Convert.ToString(_userOptions.UpdateInterval);
			_optionsControlUpdating.trackUpdateInterval.Value   = _userOptions.UpdateInterval;

			// Grab the focus
			this.Focus();

			// Select the node zero
			treeOptions.SelectedNode = treeOptions.Nodes[0];
			treeOptions.Select();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			ValidateAndSaveChanges();
			this.Close();
		}

		private void btnOKAndUpdate_Click(object sender, System.EventArgs e)
		{
			ValidateAndSaveChanges();
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void treeOptions_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// Clear out the panel first
			for(int i = 0; i < panelOptions.Controls.Count; i++)
			{
				panelOptions.Controls[i].Hide();
			}
			panelOptions.Controls.Clear();

			switch(e.Node.Index)
			{
				case 0:
					_optionsControlUi.Dock = DockStyle.Fill;
					_optionsControlUi.Show();
					panelOptions.Controls.Add(_optionsControlUi);
					panelOptions.BringToFront();
					_optionsControlUi.BringToFront();
					break;
				case 1:
					_optionsControlUpdating.Dock = DockStyle.Fill;
					_optionsControlUpdating.Show();
					panelOptions.Controls.Add(_optionsControlUpdating);
					panelOptions.BringToFront();
					_optionsControlUpdating.BringToFront();
					break;
				case 2:
					_optionsControlStartup.Dock = DockStyle.Fill;
					_optionsControlStartup.Show();
					panelOptions.Controls.Add(_optionsControlStartup);
					panelOptions.BringToFront();
					_optionsControlStartup.BringToFront();
					break;
			}
		}

		private void treeOptions_BeforeCollapse(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			e.Cancel = true;
		}
		#endregion

		#region Utility Methods
		private void ValidateAndSaveChanges()
		{
			_userOptions.CheckVersionAndUrl = _optionsControlStartup.chkCheckVersionAndUrl.Checked;
			_userOptions.IconChangeOption   = (IconChangeOptions)_optionsControlUpdating.comboIconChanges.SelectedIndex + 1;
			_userOptions.TemperatureScale   = _optionsControlUi.radioFarenheit.Checked ? TemperatureScales.Farenheit : TemperatureScales.Celsius;
			_userOptions.IconFlashFrequency = Utils.TextToInt(_optionsControlUpdating.txtIconFlashFrequency.Text, Constants.MIN_ICONFLASHFREQUENCY, Constants.MAX_ICONFLASHFREQUENCY, Constants.MIN_ICONFLASHFREQUENCY);
			_userOptions.NewVersionPopup    = _optionsControlStartup.chkNewVersionPopup.Checked;
			_userOptions.Postcode           = _optionsControlUpdating.txtPostcode.Text.ToUpper();
			_userOptions.RunAtSystemStartup = _optionsControlStartup.chkRunAtStartup.Checked;
			_userOptions.UpdateInterval     = Utils.TextToInt(_optionsControlUpdating.txtUpdateInterval.Text, Constants.MIN_UPDATEINTERVAL, Constants.MAX_UPDATEINTERVAL, Constants.MIN_UPDATEINTERVAL);
		}
		#endregion

		#region Properties
		internal UserOptions UserOptions
		{
			get
			{
				return _userOptions;
			}
		}
		#endregion
	}
}
