using System;
using Microsoft.Win32;       // RegistryKey
using System.Windows.Forms;  // MessageBox
using System.ComponentModel; // Description
using System.Drawing;        // Color, Icon
using System.Reflection;     // FieldInfo
using System.Diagnostics;    // EventLogEntryType
using System.Net;            // Webrequest
using System.IO;             // Stream
using System.Text.RegularExpressions; // Regex



namespace Mossywell
{
	namespace UKWeather
	{
		internal class Utils
		{
			#region Class Fields
			#endregion

			#region Constructor
			internal Utils()
			{
			}
			#endregion

			#region Utility Methods
			internal static void LaunchNewBrowser(string url)
			{
				// This launches the default browser in a new window - always!
				string strValue      = "";
				string strDefBrowser = "";
				RegistryKey rk;
				Regex re;
			
				// Get the default browser from the registry
				try
				{
					rk = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command");
					strValue = rk.GetValue("").ToString();
				}
				catch
				{
					EventLog.WriteEntry(Application.ProductName, "Unable to ascertain the default browser.", EventLogEntryType.Error);
				}

				// Match string with spaces surrounded by quotes (IE)
				re = new Regex("^\"[^\"]*\"");
				strDefBrowser = re.Match(strValue).ToString();

				if(strDefBrowser == "")
				{
					// No string surrounded by quotes! Try without quotes (Mozilla)...
					re = new Regex("^[^ ]*");
					strDefBrowser = re.Match(strValue).ToString();
				}

				if(strDefBrowser == "")
				{
					// Something's gone wrong so force use IE to be safe!
					strDefBrowser = "explorer.exe";
				}

				Process proc                   = new Process();
				proc.StartInfo.Arguments       = url;
				proc.StartInfo.CreateNoWindow  = false;
				proc.StartInfo.UseShellExecute = true;
				proc.StartInfo.WindowStyle     = ProcessWindowStyle.Maximized;

				// Let's go for it!
				try
				{
					proc.StartInfo.FileName        = strDefBrowser;
					proc.Start();
				}
				catch
				{
					// Probably rubbish in the strDefBrowser? Just bail out!
					EventLog.WriteEntry(Application.ProductName, "Unable to open URL " + url + ".", EventLogEntryType.Error);
				}
			}

			internal static int TextToInt(string texttoconvert, int defaultval, int maxval, int minval)
			{
				// If the text is blank or not a number, it returns the default value
				int i;

				if(texttoconvert == "")
				{
					return defaultval;
				}
				else
				{
					try
					{
						i = Convert.ToInt32(texttoconvert);  // KeyPress ensures numbers only
					}
					catch
					{
						return defaultval;
					}
				}
				if(i < minval) i = minval;
				if(i > maxval) i = maxval;

				return i;
			}

			internal static int GetSetRegistryStringValue(string regvalue, int defaultval, int maxval, int minval)
			{
				// Method fields
				bool   blnWriteBack;
				bool   blnValidRegKeyFound;
				string strValue;
				int    intRetVal;

				// Set write back to registry flag
				blnWriteBack = true;

				// Read the value from the registry
				strValue = Utils.GetRegStringValue(Constants.REG_PARAMS, regvalue);

				// Did we find anything useful in the registry?
				if(strValue == null || strValue == "")
				{
					intRetVal = defaultval;
					blnValidRegKeyFound = false;
				}
				else
				{
					// Do a try in case there's rubbish in the registry
					try
					{
						intRetVal = Convert.ToInt32(strValue);
						blnValidRegKeyFound = true;
					}
					catch
					{
						intRetVal = defaultval;
						blnValidRegKeyFound = false;
					}
				}

				// Is the value valid? If not, make it valid! (It will already be an int by this stage)
				if(intRetVal > maxval)
				{
					intRetVal = maxval;
				}
				else if(intRetVal < minval)
				{
					intRetVal = minval;
				}
				else if(blnValidRegKeyFound)
				{
					// Must be a valid value that was found in the registry
					blnWriteBack = false;
				}

				// Write it back if necessary
				if(blnWriteBack)
				{
					Utils.SetRegStringValue(Constants.REG_PARAMS, regvalue, intRetVal.ToString());
				}

				return intRetVal;
			}

			internal static string GetExecutableLocation()
			{
				string str = System.Reflection.Assembly.GetExecutingAssembly().Location;
				return str.Substring(0, str.LastIndexOf(@"\"));
			}

			internal static string GetEnumDescription(Enum value)
			{
				FieldInfo fi = value.GetType().GetField(value.ToString());
				DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
				return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
			}

			internal static void GetHelp(Control parent, System.Windows.Forms.HelpEventArgs hlpevent)
			{
				Help.ShowHelp(parent, Utils.GetExecutableLocation() + @"\UKWeather.chm", HelpNavigator.Topic, null);
				if(hlpevent != null)
					hlpevent.Handled = true;
			}

			internal static void GetHelp(Control parent, System.Windows.Forms.HelpEventArgs hlpevent, HelpFile helpfile)
			{
				Help.ShowHelp(parent, Utils.GetExecutableLocation() + @"\UKWeather.chm", HelpNavigator.Topic, (object)(GetEnumDescription(helpfile)));
				if(hlpevent != null)
					hlpevent.Handled = true;
			}

			internal static string GetRegStringValue(string regkey, string regvalue)
			{
				// Return values:
				//   Value not found:         ""
				//   Registry not accessible: null
				string strValue;
        RegistryKey rk = null;

				try
				{
					rk = Registry.CurrentUser.OpenSubKey(regkey);
					strValue = rk.GetValue(regvalue, "").ToString();
				}
				catch
				{
					strValue = null;
				}

				if(rk != null) rk.Close();

				return strValue;
			}

			internal static void SetRegStringValue(string regkey, string regvalue, string data)
			{
				RegistryKey rk = null;

				if(data == null)
				{
					try
					{
						rk = Registry.CurrentUser.OpenSubKey(regkey, true);
						rk.DeleteValue(regvalue, false);
					}
					catch
					{
						FormMain.SendToEventLog("Error deleting from the registry. The key that should have been deleted from is:" +
							Environment.NewLine + regkey +
							Environment.NewLine + "The value that should have been deleted is:" + 
							Environment.NewLine + regvalue, EventLogEntryType.Error);
					}
				}
				else
				{
					try
					{
						rk = Registry.CurrentUser.OpenSubKey(regkey, true);
						rk.SetValue(regvalue, Convert.ToString(data));
					}
					catch
					{
						FormMain.SendToEventLog("Error writing to the registry. The key that should have been written to is:" +
							Environment.NewLine + regkey +
							Environment.NewLine + "The value that should have been written to is:" + 
							Environment.NewLine + regvalue +
							Environment.NewLine + "The data that should have been written are:" +
							Environment.NewLine + data, EventLogEntryType.Error);
					}
				}

				if(rk != null) rk.Close();
			}
			#endregion
		}
	}
}
