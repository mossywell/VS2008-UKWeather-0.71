using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;                     // Webrequest
using System.Text.RegularExpressions; // Regex
using System.Reflection;              // Assembly
using System.Drawing.Imaging;         // PixelFormat
using System.Diagnostics;             // Process, EventLogEntryType
using System.Runtime.InteropServices; // DllImport
using Microsoft.Win32;                // RegistryKey
using System.IO;                      // TextWriter
using System.Threading;               // Thread, ThreadState

namespace Mossywell.UKWeather
{
	internal class FormMain : System.Windows.Forms.Form
	{
		#region Externals
		[DllImport("user32.dll")]
		private static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszWindow, string lpszClass); 
		[DllImport("user32.dll")]
		private static extern int GetSystemMenu(int hwnd, int revert);
		[DllImport("user32.dll")]
		private static extern int EnableMenuItem(int menu, int ideEnableItem, int enable);
		#endregion

		#region Class Fields
		private delegate void GetUKWDataCallbackDelegate(string webresponse, string errors);
		private FormStatus _frmStatus;

		private const int SC_CLOSE     = 0xF060;
		private const int MF_BYCOMMAND = 0x0;
		private const int MF_GRAYED    = 0x1;
		private const int MF_ENABLED   = 0x0; 

		private const string               FONT            = "Arial";
		private const int                  MAX_FONT_SIZE   = 10;
		
		private Color  COLOR_BW_FLASH_ON  = Color.Black;
		private Color  COLOR_BW_FLASH_OFF = Color.White;
		private Color  COLOR_BORDER       = Color.FromArgb(128, 128, 128);
		private string DEG                = (char)0186 + "";

		private string               _strUrl                = Constants.URL_DATA;
		private bool                 _blnMouseMovedOverIcon = false;
		private Icon                 _iconFlashOn           = null;
		private Icon                 _iconFlashOff          = null;
		private FormAbout            _frmAbout              = null;
		private FormOptions          _frmOptions            = null;
		private FormColours          _frmColours            = null;
		private LogLevel             _logLevel;
		private string               _strLogFile            = Utils.GetExecutableLocation() + @"\" + Application.ProductName + ".log";
		private UserOptions          _userOptions;
		private NotifyIconParameters _nip;
		private Thread               _dataUpdaterThread     = null;

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenu contextMenuIconNotify;
		private System.Windows.Forms.Timer timerGetData;
		private System.Windows.Forms.Timer timerSetIconColor;
		private System.Windows.Forms.RichTextBox rtxtMain;
		private System.ComponentModel.IContainer components;
		#endregion

		#region Constructor
		internal FormMain()
		{
			InitializeComponent();
			NotifyIconMenus();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMain));
			this.timerGetData = new System.Windows.Forms.Timer(this.components);
			this.timerSetIconColor = new System.Windows.Forms.Timer(this.components);
			this.rtxtMain = new System.Windows.Forms.RichTextBox();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.SuspendLayout();
			// 
			// timerGetData
			// 
			this.timerGetData.Tick += new System.EventHandler(this.timerGetData_Tick);
			// 
			// timerSetIconColor
			// 
			this.timerSetIconColor.Tick += new System.EventHandler(this.timerSetIconColor_Tick);
			// 
			// rtxtMain
			// 
			this.rtxtMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtxtMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtMain.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rtxtMain.Location = new System.Drawing.Point(0, 0);
			this.rtxtMain.Name = "rtxtMain";
			this.rtxtMain.Size = new System.Drawing.Size(584, 190);
			this.rtxtMain.TabIndex = 0;
			this.rtxtMain.Text = "";
			// 
			// notifyIcon
			// 
			this.notifyIcon.Text = "";
			this.notifyIcon.Visible = true;
			// 
			// FormMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(584, 190);
			this.Controls.Add(this.rtxtMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormMain_Closing);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FormMain_HelpRequested);
			this.ResumeLayout(false);

		}
		#endregion

		#region Notify Icon Initialisation
		private void NotifyIconMenus()
		{
			// Create the context menus
			MenuItem[] mi = new MenuItem[11];

			mi[0] = new MenuItem();
			mi[0].Text = "Options";
			mi[0].Click += new System.EventHandler(this.MenuOptions);

			mi[1] = new MenuItem();
			mi[1].Text = "Colour Scheme";
			mi[1].Click += new System.EventHandler(this.MenuColours);
			
			mi[2] = new MenuItem();
			mi[2].Text = "Update Now";
			mi[2].Click += new System.EventHandler(this.MenuUpdateNow);

			mi[3] = new MenuItem();
			mi[3].Text = "-";
			
			mi[4] = new MenuItem();
			mi[4].Text = "Weather Web Site";
			mi[4].Click += new System.EventHandler(this.MenuWeatherWebSite);
			
			mi[5] = new MenuItem();
			mi[5].Text = "Mossywell Web Site";
			mi[5].Click += new System.EventHandler(this.MenuMossywellWebSite);
			
			mi[6] = new MenuItem();
			mi[6].Text = "-";
			
			mi[7] = new MenuItem();
			mi[7].Text = "Help";
			mi[7].Click += new System.EventHandler(this.MenuHelp);
			
			mi[8] = new MenuItem();
			mi[8].Text = "About...";
			mi[8].Click += new System.EventHandler(this.MenuAbout);
			
			mi[9] = new MenuItem();
			mi[9].Text = "-";
			
			mi[10] = new MenuItem();
			mi[10].Text = "Exit";
			mi[10].Click += new System.EventHandler(this.MenuExitNotify);

			contextMenuIconNotify = new ContextMenu(mi);

			notifyIcon.ContextMenu = contextMenuIconNotify;
			notifyIcon.DoubleClick += new System.EventHandler(this.ni_DoubleClick);
			notifyIcon.MouseMove   += new MouseEventHandler(this.ni_MouseMove);
		}
		#endregion

		#region Events
		private void FormMain_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
		{
		}

		protected override void OnActivated(System.EventArgs e)
		{
			this.Hide();
		}

		private void FormMain_Load(object sender, System.EventArgs e)
		{
			// Hide the main message form
			this.Visible = false;
		
			// Give the form a title - needed for checking if it's already running
			this.Text = Constants.MAIN_FORM_NAME;

			// Set the defaults
			// The _wpSavedStatus ensures that if there's no network when the app is started,
			// it is logged and also the no network icon and text are showed
			_nip.NotifyIconTextHasChanged      = false;
			_nip.LastIconTimeString            = new TimeString();
			_nip.PostcodeChangedSinceNoNetwork = false;
			_nip.NoNetworkHasBeenLogged        = false;
			_nip.LastStatus                    = WebParserStatus.BadPostcode;
			_nip.LastIconText                  = "DUMMY-DATA";
		
  		// Create the parameters registry key
			try
			{
				Registry.CurrentUser.CreateSubKey(Constants.REG_PARAMS);
			}
			catch
			{
				LogDisaster("Unable to create or access the application registry key." + Environment.NewLine + 
					"UK Weather will try to run without it. Expect plenty of errors in the Event Log!");
  		}


			// Initialisation stuff start here:
			DoLogLevel();

			// Set the user options...
			DoCheckVersionAndUrl();
			DoIconChangeOption();
			DoTemperatureScale();
			DoIconFlashFrequency();
			DoNewVersionPopup();
			DoPostcode();
			DoRunAtStartup();
			DoUpdateInterval();

			// Now check the version and URL...
			DoGetUKWeatherInfo();

			// Make the first lot of icons and then start the icon flash timer
			MakeIconsSafelyAndSaveGlobals(Constants.CHAR_OBTAININGDATA);
			notifyIcon.Icon = this._iconFlashOff;
			notifyIcon.Text = _nip.LastIconTimeString.WholeTimeString + Environment.NewLine + "Retrieving data...";
			timerSetIconColor.Start();

			// Do an immediate update (starts the getdata timer)
			UpdateNow();
		}
		
		private void FormMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Kill the system tray icon
			notifyIcon.Visible = false;
		}

		private void ni_DoubleClick(object sender, System.EventArgs e)
		{
			// Simply call the appropriate menu method
			MenuWeatherWebSite(null, null);
		}

		private void ni_MouseMove(object sender, MouseEventArgs e)
		{
			_blnMouseMovedOverIcon = true;
		}

		private void timerSetIconColor_Tick(object sender, System.EventArgs e)
		{
			// NOTE: This runs in the UI (main form) thread and uses its message pump

			// Always flash if this option selected
			if(_userOptions.IconChangeOption == IconChangeOptions.FlashContinuously)
			{
				notifyIcon.Icon = notifyIcon.Icon == _iconFlashOn ? _iconFlashOff : _iconFlashOn;
			}
			// The more normal case that FlashContinuously isn't selected!
			else if(_nip.NotifyIconTextHasChanged)
			{
				switch(_userOptions.IconChangeOption)
				{
					case IconChangeOptions.DoNothing:
						notifyIcon.Icon = _iconFlashOff;
						_nip.NotifyIconTextHasChanged = false;
						break;

					case IconChangeOptions.ChangeColour:
						if(_blnMouseMovedOverIcon)
						{
							notifyIcon.Icon = _iconFlashOff;
							_nip.NotifyIconTextHasChanged = false;
						}
						else
						{
							notifyIcon.Icon = _iconFlashOn;
						}
						break;

					case IconChangeOptions.Flash:
						if(_blnMouseMovedOverIcon)
						{
							notifyIcon.Icon = _iconFlashOff;
							_nip.NotifyIconTextHasChanged = false;
						}
						else
						{
							notifyIcon.Icon = notifyIcon.Icon == _iconFlashOn ? _iconFlashOff : _iconFlashOn;
						}
						break;
				}
			}

			// Reset this flag every time we run
			this._blnMouseMovedOverIcon = false;
		}

		private void timerGetData_Tick(object sender, System.EventArgs e)
		{
			this.UpdateDataAndNotifyIcon();
		}
		#endregion
	
		#region Main
		[STAThread]
		static void Main() 
		{
			// Only run if I'm not already running
			int hWnd = FindWindowEx(0, 0, null, Constants.MAIN_FORM_NAME);
			if(hWnd == 0)
			{
				Application.EnableVisualStyles();
				Application.DoEvents();
				Application.Run(new FormMain());
			}
			else
			{
				MessageBox.Show("You can only run me once and I'm already running!" + Environment.NewLine +
					"Look down in the System Tray - you should see me there."  + Environment.NewLine +
					"If not, do a CTRL-ALT-DEL and look for UKWeather.exe.",
					"UK Weather",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
			}
		}
		#endregion

		#region Utility Methods
		private void LogDisaster(string msg)
		{
			SendToEventLog(msg, EventLogEntryType.Error);
			MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void Log(string msg, LogLevel logimportance)
		{
			// Only log if the logimportance =< _logLevel, but logimportance of None (0) is never logged
			// Therefore,
			// _logLevel = None (0) means never log anything
			// _logLevel = Error (1) means log logimportance of Error (1) only
			// _logLevel = Warning (2) means log logimportances of Error (1) and Warning (2) only
			// _logLevel = Information (3) means log logimportances of Error (1), Warning (2) and Information (3) only
			// _logLevel = KitchenSink (4) means log logimportances of 1, 2, 3 and 4 only

			if(logimportance == LogLevel.None) return;

			if((int)logimportance <= (int)_logLevel)
			{
				StreamWriter sw = null;

				try
				{
					sw = new StreamWriter(_strLogFile, true);
					sw.WriteLine(DateTime.Now + " - " + msg);
				}
				catch
				{
					LogDisaster("Unable to write to the log file.");
				}

				if(sw != null) sw.Close();
			}
		}

		private void DoLogLevel()
		{
			// Currently, the logging level is not used.
			// Not written back to the registry by default
			string strValue = Utils.GetRegStringValue(Constants.REG_PARAMS, Constants.REG_LOG);
			if(strValue == null || strValue == "")
			{
				_logLevel = Constants.DEFAULT_LOGLEVEL;
			}
			else
			{
				try
				{
					switch(strValue)
					{
						case "None":
							_logLevel = LogLevel.None;
							break;
						case "Error":
							_logLevel = LogLevel.Error;
							break;
						case "Warning":
							_logLevel = LogLevel.Warning;
							break;
						case "Information":
							_logLevel = LogLevel.Information;
							break;
						case "Kitchen Sink":
							_logLevel = LogLevel.KitchenSink;
							break;
						default:
							_logLevel = Constants.DEFAULT_LOGLEVEL;
							break;
					}
				}
				catch
				{
					_logLevel = Constants.DEFAULT_LOGLEVEL;
				}
			}

			if(_logLevel != LogLevel.None)
			{
				SendToEventLog("Logging level set to \"" + strValue + "\"." + Environment.NewLine +
					"Logging to file \"" + _strLogFile + "\".", EventLogEntryType.Warning);
			}
		}

		private void DoGetUKWeatherInfo()
		{
			if(_userOptions.CheckVersionAndUrl)
			{
				// This next line needs to be run as a new thread ASYNCHRONOUSLY
				Thread thrd = new Thread(new ThreadStart(GetUKWDataAsNewThread));
				thrd.Name   = "UKWInfo";
				thrd.Start();

				// Show the status form on MAIN thread
				_frmStatus = new FormStatus();
				_frmStatus.ShowDialog(this); // Main thread WAITS for this dialog to close
			}
		}

		private void DoCheckVersionAndUrl()
		{
			if(Utils.GetSetRegistryStringValue(Constants.REG_CHECKVERANDURL, Constants.DEFAULT_CHECKVERANDURL, 1, 0) == 0)
				_userOptions.CheckVersionAndUrl = false;
			else
				_userOptions.CheckVersionAndUrl = true;
		}

		private void DoIconChangeOption()
		{
			// Method fields
			bool   blnWriteBack;
			bool   blnValidRegKeyFound;
			string strValue;

			// Set write back to registry flag
			blnWriteBack = true;

			// Read the value from the registry
			strValue = Utils.GetRegStringValue(Constants.REG_PARAMS, Constants.REG_ICONCHANGE);

			// Did we find anything useful in the registry?
			if(strValue == null || strValue == "")
			{
				_userOptions.IconChangeOption = Constants.DEFAULT_ICONCHANGE;
				blnValidRegKeyFound = false;
			}
			else
			{
				bool blnValidValue = false;
				IconChangeOptions ico;
				for(ico = IconChangeOptions.FirstValue; ico < IconChangeOptions.LastValue; ico++)
				{
					if(ico != IconChangeOptions.FirstValue && ico != IconChangeOptions.LastValue)
					{
						if(strValue == ico.ToString())
						{
							_userOptions.IconChangeOption = ico;
							blnValidValue = true;
						}
					}
				}
				if(blnValidValue)
				{
					blnValidRegKeyFound = true;
				}
				else
				{
					_userOptions.IconChangeOption = Constants.DEFAULT_ICONCHANGE;
					blnValidRegKeyFound = false;
				}
			}

			// Should we write the value back to the registry?
			if(blnValidRegKeyFound)
			{
				// Must be a valid value that was found in the registry
				blnWriteBack = false;
			}

			// Write it back if necessary
			if(blnWriteBack)
			{
				Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_ICONCHANGE, _userOptions.IconChangeOption.ToString());
			}
		}

		private void DoTemperatureScale()
		{
			// Method fields
			bool   blnWriteBack;
			bool   blnValidRegKeyFound;
			string strValue;

			// Set write back to registry flag
			blnWriteBack = true;

			// Read the value from the registry
			strValue = Utils.GetRegStringValue(Constants.REG_PARAMS, Constants.REG_TEMPERATURESCALE);

			// Did we find anything useful in the registry?
			if(strValue == null || strValue == "")
			{
				_userOptions.TemperatureScale = Constants.DEFAULT_TEMPERATURESCALE;
				blnValidRegKeyFound = false;
			}
			else
			{ 
				if(strValue == TemperatureScales.Celsius.ToString())
				{
					_userOptions.TemperatureScale = TemperatureScales.Celsius;
					blnValidRegKeyFound = true;
				}
				else if(strValue == TemperatureScales.Farenheit.ToString())
				{
					_userOptions.TemperatureScale = TemperatureScales.Farenheit;
					blnValidRegKeyFound = true;
				}
				else
				{
					_userOptions.TemperatureScale = Constants.DEFAULT_TEMPERATURESCALE;
					blnValidRegKeyFound = false;
				}
			}

			// Should we write the value back to the registry?
			if(blnValidRegKeyFound)
			{
				// Must be a valid value that was found in the registry
				blnWriteBack = false;
			}

			// Write it back if necessary
			if(blnWriteBack)
			{
				Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_TEMPERATURESCALE, _userOptions.TemperatureScale.ToString());
			}
		}

		private void DoIconFlashFrequency()
		{
			_userOptions.IconFlashFrequency = Utils.GetSetRegistryStringValue(Constants.REG_ICONFLASHFREQUENCY, Constants.DEFAULT_ICONFLASHFREQUENCY, Constants.MAX_ICONFLASHFREQUENCY, Constants.MIN_ICONFLASHFREQUENCY);
			timerSetIconColor.Interval = _userOptions.IconFlashFrequency;
		}

		private void DoNewVersionPopup()
		{
			if(Utils.GetSetRegistryStringValue(Constants.REG_NEWVERSIONPOPUP, Constants.DEFAULT_NEWVERSIONPOPUP, 1, 0) == 0)
			  _userOptions.NewVersionPopup = false;
			else
				_userOptions.NewVersionPopup = true;
		}

		private void DoPostcode()
		{
			// Method fields
			bool   blnWriteBack;
			bool   blnValidRegKeyFound;
			string strValue;

			// Set write back to registry flag
			blnWriteBack = true;

			// Read the value from the registry
			strValue = Utils.GetRegStringValue(Constants.REG_PARAMS, Constants.REG_POSTCODE);

			// Did we find anything useful in the registry?
			if(strValue == null || strValue == "")
			{
				_userOptions.Postcode = Constants.DEFAULT_POSTCODE;
				blnValidRegKeyFound = false;
			}
			else
			{
				if(strValue.Length > 4)
				{
					_userOptions.Postcode = strValue.Substring(0, 4);
					blnValidRegKeyFound = false;
				}
				else
				{
					_userOptions.Postcode = strValue;
					blnValidRegKeyFound = true;
				}
			}

			// Should we write the value back to the registry?
			if(blnValidRegKeyFound)
			{
				// Must be a value that was found in the registry
				blnWriteBack = false;
			}

			// Write it back if necessary
			if(blnWriteBack)
			{
				Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_POSTCODE, _userOptions.Postcode);
			}

			// Set the saved postcode to whatever we've set it to here
			_nip.LastPostcode = _userOptions.Postcode;
		}

		private void DoRunAtStartup()
		{
			string strValue = Utils.GetRegStringValue(Constants.REG_RUN, Application.ProductName);
			
			if(strValue == null)
			{
				// Problem accessing the registry
				_userOptions.RunAtSystemStartup = false;
			}
			else if(strValue == "")
			{
				// Blank value
				_userOptions.RunAtSystemStartup = Constants.DEFAULT_RUNATSYSTEMSTARTUP;
			}
			else
			{
				// Something in it
				if(strValue != Environment.CommandLine.TrimEnd())
				{
					_userOptions.RunAtSystemStartup = false;
				}
				else
				{
					_userOptions.RunAtSystemStartup = true;
				}
			}
		}

		private void DoUpdateInterval()
		{
			_userOptions.UpdateInterval = Utils.GetSetRegistryStringValue(Constants.REG_UPDATEINTERVAL, Constants.DEFAULT_UPDATEINTERVAL, Constants.MAX_UPDATEINTERVAL, Constants.MIN_UPDATEINTERVAL);
			timerGetData.Interval = _userOptions.UpdateInterval * 1000;
		}

		private void GetUKWDataAsNewThread()
		{
			// Runs on separate thread
			string strResponse = "";
			string strError = "";

			try
			{
				strResponse = WebUtils.GetURI(Constants.URL_UKW_INFO, Constants.DEFAULT_UKWDATATIMEOUT);
			}
			catch(Exception ex)
			{
				strError = ex.Message;
			}

			GetUKWDataCallbackDelegate gukwcd = new GetUKWDataCallbackDelegate(GetUKWDataCallback);

			// Let's invoke SYNCHRONOUSLY on the main thread as we'll be updating the UI
			this.Invoke(gukwcd, new object[] {strResponse, strError});
		}

		private void GetUKWDataCallback(string webreponse, string errors)
		{
			// Invoked to run on main thread
			Regex re = null;

			// Look after the status form if it still exists
			if(!_frmStatus.IsDisposed)
			{
				if(_frmStatus.Visible)
				{
					// Disable the close button and fill the progress bar
					try
					{
						EnableMenuItem(GetSystemMenu(_frmStatus.Handle.ToInt32(), 0), SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
					}
					catch
					{
						// No worries if EnableMenuItem bombs out
					}
					_frmStatus.Stop();
					_frmStatus.FillProgressBar();
					_frmStatus.AddText(errors);

					// Pause the MAIN THREAD
					if(errors == "")
						Thread.Sleep(Constants.THREAD_SLEEP_NO_ERRORS);
					else
						Thread.Sleep(Constants.THREAD_SLEEP_ERRORS);

					// Close the form
					_frmStatus.Close();
				}

				// Dispose of the form
				_frmStatus.Dispose();
			}

			// Now deal with the returned data
			if(webreponse != "")
			{
				// Do the version check first
				if(_userOptions.NewVersionPopup)
				{
					// According to the registry, it's OK to check the version
					re = new Regex(Constants.REGEX_VERSION, RegexOptions.Multiline);
					string strLatestVersion = re.Match(webreponse).Groups[1].Value.ToString();
					string strThisVersion   = Application.ProductVersion;
					if(strLatestVersion != strThisVersion)
					{
						FormVersion frm = new FormVersion(strThisVersion, strLatestVersion);
						if(frm.ShowDialog(this) == DialogResult.Yes)
						{
							Utils.LaunchNewBrowser(Constants.URL_UKW_DOWNLOAD);
						}
						if(frm.DoVersionCheck == "0")
							_userOptions.NewVersionPopup = false;
						else
							_userOptions.NewVersionPopup = true;
						Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_NEWVERSIONPOPUP, frm.DoVersionCheck);
						frm.Dispose();
					}
				}

				// Now do the URL check
				re = new Regex(Constants.REGEX_URL, RegexOptions.Multiline);
				string strUrl = re.Match(webreponse).Groups[1].Value.ToString();
				if(strUrl != "")
				{
					// Write it to the class field, but don't save it in the registry as
					// it'll be picked up each time UKW starts anyway. However, do note it
					// in the Event Log.
					_strUrl = strUrl;
					SendToEventLog(Application.ProductName + " will use the following URL: " +
						Environment.NewLine +
						strUrl + Environment.NewLine + 
            "This URL will override the default URL.", EventLogEntryType.Information);
				}
			}
		}

		private void UpdateNow()
		{
			// Reset the timer and then do an immediate update
			timerGetData.Stop();
			timerGetData.Start();
			UpdateDataAndNotifyIcon();
		}

		private void UpdateDataAndNotifyIcon()
		{
			// Kill the old thread if it's still running
			if(_dataUpdaterThread != null && _dataUpdaterThread.ThreadState != System.Threading.ThreadState.Stopped)
			{
				if(_dataUpdaterThread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
				{
				  // Interrupt a waitsleepjoin
				  _dataUpdaterThread.Interrupt();
				}
				else
				{
					// Log non-waitsleepjoin
					FormMain.SendToEventLog("The state of the Data Updater thread is: " +
						Environment.NewLine +
						_dataUpdaterThread.ThreadState.ToString() +
						Environment.NewLine +
						"The thread will be aborted.", EventLogEntryType.Error);
				}

				// Kill it
				_dataUpdaterThread.Abort();

				// Wait for it to finish to be safe
				_dataUpdaterThread.Join();
			}

			// At last - create the new thread
			DataUpdater du = new DataUpdater(_userOptions.Postcode, _userOptions.TemperatureScale, _strUrl, _nip, new DataUpdaterCallbackDelegate(DataUpdaterCallback));
			_dataUpdaterThread = new Thread(new ThreadStart(du.DoTheUpdate));
			_dataUpdaterThread.Start();
		}

		private void DataUpdaterCallback(NotifyIconParameters nip)
		{
			if(this.InvokeRequired)
			{
				// Invoke required
				DataUpdaterCallbackDelegate ducd = new DataUpdaterCallbackDelegate(DataUpdaterCallback);
				this.Invoke(ducd, new object[] {nip});
			}
			else
			{
				// Overwrite the current notify icon parameters with
				// the results of the latest data fetch
				_nip = nip;
				if(_nip.NotifyIconTempHasChanged)
				{
					MakeIconsSafelyAndSaveGlobals(_nip.LastIconTemp);
				}
				notifyIcon.Text = _nip.CompleteNotifyIconText;
			}
		}

		private void MakeIconsSafelyAndSaveGlobals(string message)
		{
			// Temporarily prevent the icon timer from running & wait for it to finish
			this.timerSetIconColor.Stop();
			while(this.timerSetIconColor.Enabled) {}

			// Make the icons
			IconPair ip = MakeIcons(message);
			_iconFlashOn  = ip.OnIcon;
			_iconFlashOff = ip.OffIcon;

			// OK for the icon timer to continue now that the new icons are made
			this.timerSetIconColor.Start();
		}

		internal IconPair MakeIcons(string message)
		{
			Bitmap       bmp = new Bitmap(16, 16);
			Graphics     g   = Graphics.FromImage(bmp);
			Rectangle    r   = new Rectangle(0, 0, bmp.Width, bmp.Height);
			SolidBrush   b, btext;
			StringFormat sf;
			Font         f;
			IconPair     ip  = new IconPair();

			// Set the colour to use
			Color        colorOn;
			Color        colorOff;
			Color        colorTextOn;
			Color        colorTextOff;
			int          intTemp = Int32.MinValue; // Some arbitrary value that we can test for
			try
			{
				intTemp = Convert.ToInt32(message);
			}
			catch {}
			if(intTemp == Int32.MinValue)
			{
				colorOn  = COLOR_BW_FLASH_ON;
				colorOff = COLOR_BW_FLASH_OFF;
				colorTextOn  = COLOR_BW_FLASH_OFF;
				colorTextOff = COLOR_BW_FLASH_ON;
			}
			else
			{
				ColorOnOff colours = this.CalculateRGBValue(intTemp);
				colorOn      = colours.OnColor;
				colorOff     = colours.OffColor;
				colorTextOn  = Color.Black;
				colorTextOff = Color.Black;
			}
		
			// sf = new StringFormat(StringFormat.GenericTypographic);
			sf = new StringFormat();
			sf.Alignment     = StringAlignment.Center;   // Horitontal
			sf.LineAlignment = StringAlignment.Center;   // Vertical
		
			int intFontSize = MAX_FONT_SIZE;  // Maximum size that I think looks OK
			f = new Font(FONT, intFontSize);

			// Get the maximum font size based on the string to draw - will be 8, 7 or 6
			float fltStrH = g.MeasureString(message, f, r.Height, sf).Height;
			float fltStrW = g.MeasureString(message, f, r.Width, sf).Width;
			while((fltStrH > Constants.ICON_TEXT_WIDTH_MAX || fltStrW > Constants.ICON_TEXT_WIDTH_MAX) && intFontSize > Constants.FONT_SIZE_MIN)
			{
				intFontSize--;
				f = new Font(FONT, intFontSize);
				fltStrH = g.MeasureString(message, f, r.Height, sf).Height;
				fltStrW = g.MeasureString(message, f, r.Width, sf).Width;
			}
    
			// Flash On icon (lighter)
			b = new SolidBrush(colorOn);
			btext = new SolidBrush(colorTextOn);
			g.FillRectangle(b, r);
			g.DrawRectangle(new Pen(COLOR_BORDER), 0, 0, r.Width - 1, r.Height - 1);
			g.DrawString(message, f, btext, r, sf);
			ip.OnIcon = Icon.FromHandle(bmp.GetHicon());

			// Flash Off icon (normal)
			b = new SolidBrush(colorOff);
			btext = new SolidBrush(colorTextOff);
			g.FillRectangle(b, r);
			g.DrawRectangle(new Pen(COLOR_BORDER), 0, 0, r.Width - 1, r.Height - 1);
			g.DrawString(message, f, btext, r, sf);
			ip.OffIcon = Icon.FromHandle(bmp.GetHicon());

			// Tidy up
			f.Dispose();
			sf.Dispose();
			btext.Dispose();
			b.Dispose();
			g.Dispose();
			bmp.Dispose();

			// Return
			return ip;
		}

		internal ColorOnOff CalculateRGBValue(int temp)
		{
			int intROff = 0, intGOff = 0, intBOff = 0;
			int intROn  = 0, intGOn  = 0, intBOn  = 0;

			int limit1, limit2, limit3, limit4, limit5;
			if(_userOptions.TemperatureScale == TemperatureScales.Farenheit)
			{
				limit1 = 14;
				limit2 = 32;
				limit3 = 41;
				limit4 = 59;
				limit5 = 77;
			}
			else
			{
				limit1 = -10;
				limit2 = 0;
				limit3 = 5;
				limit4 = 15;
				limit5 = 25;
			}

			if(temp >= limit5)
			{
				intROff = 255;
				intGOff = 0;
				intBOff = 0;
				intROn  = 255;
				intGOn  = 153;
				intBOn  = 0;
			}
			else if(temp >= limit4 && temp < limit5)
			{
				intROff = 255;
				intGOff = 153;
				intBOff = 0;
				intROn  = 255;
				intGOn  = 255;
				intBOn  = 51;
			}
			else if(temp >= limit3 && temp < limit4)
			{
				intROff = 255;
				intGOff = 255;
				intBOff = 51;
				intROn  = 255;
				intGOn  = 255;
				intBOn  = 255;
			}
			else if(temp >= limit2 && temp < limit3)
			{
				intROff = 153;
				intGOff = 204;
				intBOff = 255;
				intROn  = 255;
				intGOn  = 255;
				intBOn  = 255;
			}				
			else if(temp >= limit1 && temp < limit2)
			{
				intROff = 0;
				intGOff = 153;
				intBOff = 255;
				intROn  = 204;
				intGOn  = 255;
				intBOn  = 255;
			}				
			else if(temp < limit1)
			{
				intROff = 0;
				intGOff = 102;
				intBOff = 255;
				intROn  = 0;
				intGOn  = 153;
				intBOn  = 255;
			}

			ColorOnOff colours;
			colours.OffColor = Color.FromArgb(intROff, intGOff, intBOff);
			colours.OnColor  = Color.FromArgb(intROn,  intGOn,  intBOn);

			return colours;
		}
		#endregion

		#region Static Utility Methods
		static internal void SendToEventLog(string error, EventLogEntryType eventlogentrytype)
		{
			try
			{
				EventLog.WriteEntry(Application.ProductName, error, eventlogentrytype);
			}
			catch {}
		}
		#endregion

		#region Icon Menu Methods
		private void MenuExitNotify(object sender, System.EventArgs e)
		{
			notifyIcon.Visible = false;
			this.Close();
		}

		private void MenuOptions(object sender, System.EventArgs e)
		{
			if(_frmOptions == null || _frmOptions.IsDisposed)
			{
				_frmOptions = new FormOptions(_userOptions);
				_frmOptions.ShowDialog(this);

				// OK, we're back from the options form, so grab the new pre-validated values
				if(_frmOptions.DialogResult == DialogResult.OK || _frmOptions.DialogResult == DialogResult.Yes)
				{
					// Save all the settings to the registry...
					if(_userOptions.CheckVersionAndUrl != _frmOptions.UserOptions.CheckVersionAndUrl)
					{
						_userOptions.CheckVersionAndUrl = _frmOptions.UserOptions.CheckVersionAndUrl;
						if(_userOptions.CheckVersionAndUrl)
							Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_CHECKVERANDURL, "1");
						else
							Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_CHECKVERANDURL, "0");
					}

					if(_userOptions.IconChangeOption != _frmOptions.UserOptions.IconChangeOption)
					{
						_userOptions.IconChangeOption = _frmOptions.UserOptions.IconChangeOption;
						Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_ICONCHANGE, _userOptions.IconChangeOption.ToString());
					}

					if(_userOptions.TemperatureScale != _frmOptions.UserOptions.TemperatureScale)
					{
						_userOptions.TemperatureScale = _frmOptions.UserOptions.TemperatureScale;
						Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_TEMPERATURESCALE, _userOptions.TemperatureScale.ToString());
					}
					
					if(_userOptions.IconFlashFrequency != _frmOptions.UserOptions.IconFlashFrequency)
					{
						_userOptions.IconFlashFrequency = _frmOptions.UserOptions.IconFlashFrequency;
						Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_ICONFLASHFREQUENCY, _userOptions.IconFlashFrequency.ToString());
						this.timerSetIconColor.Interval = _userOptions.IconFlashFrequency;
					}

					if(_userOptions.NewVersionPopup != _frmOptions.UserOptions.NewVersionPopup)
					{
						_userOptions.NewVersionPopup = _frmOptions.UserOptions.NewVersionPopup;
						if(_userOptions.NewVersionPopup)
						  Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_NEWVERSIONPOPUP, "1");
						else
							Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_NEWVERSIONPOPUP, "0");
					}

					if(_userOptions.Postcode != _frmOptions.UserOptions.Postcode)
					{
						_userOptions.Postcode = _frmOptions.UserOptions.Postcode;
						Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_POSTCODE, _userOptions.Postcode);
					}

					if(_userOptions.RunAtSystemStartup != _frmOptions.UserOptions.RunAtSystemStartup)
					{
					  _userOptions.RunAtSystemStartup = _frmOptions.UserOptions.RunAtSystemStartup;
						if(_userOptions.RunAtSystemStartup)
							Utils.SetRegStringValue(Constants.REG_RUN, Application.ProductName, Environment.CommandLine.TrimEnd());
						else
							Utils.SetRegStringValue(Constants.REG_RUN, Application.ProductName, null);
					}

					if(_userOptions.UpdateInterval != _frmOptions.UserOptions.UpdateInterval)
					{
						_userOptions.UpdateInterval = _frmOptions.UserOptions.UpdateInterval;
						Utils.SetRegStringValue(Constants.REG_PARAMS, Constants.REG_UPDATEINTERVAL, _userOptions.UpdateInterval.ToString());
						this.timerGetData.Interval = _userOptions.UpdateInterval * 1000;
					}
  			}

				// Update now?
				if(_frmOptions.DialogResult == DialogResult.Yes)
				{
					UpdateNow();
				}

				// Dispose of the form now that we've finished with it
				_frmOptions.Dispose();
			}
			else
			{
				_frmOptions.Focus();
			}
		}

		private void MenuColours(object sender, System.EventArgs e)
		{
			if(_frmColours == null || _frmColours.IsDisposed)
			{
				_frmColours = new FormColours(this, _userOptions.TemperatureScale);
				_frmColours.ShowDialog();
				_frmColours.Dispose();
			}
			else
			{
				_frmColours.Focus();
			}
		}

		private void MenuWeatherWebSite(object sender, System.EventArgs e)
		{
			Utils.LaunchNewBrowser(_strUrl + _userOptions.Postcode + "");
		}

		private void MenuMossywellWebSite(object sender, System.EventArgs e)
		{
			Utils.LaunchNewBrowser("http://www.mossywell.com");
		}

		private void MenuAbout(object sender, System.EventArgs e)
		{
			if(_frmAbout == null || _frmAbout.IsDisposed)
			{
				_frmAbout = new FormAbout(_strUrl);
				_frmAbout.ShowDialog(this);
				_frmAbout.Dispose();
			}
			else
			{
				_frmAbout.Focus();
			}
		}

		private void MenuUpdateNow(object sender, System.EventArgs e)
		{
			UpdateNow();
		}

		private void MenuHelp(object sender, System.EventArgs e)
		{
			Utils.GetHelp(this, null, HelpFile.Desktop);
		}
		#endregion

		#region Properties
		#endregion
	}
}
