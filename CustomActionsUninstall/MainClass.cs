using System;
using Microsoft.Win32;                // RegistryKey
using System.Runtime.InteropServices; // DllImport

namespace Mossywell
{
	namespace UKWeather
	{
		/// <summary>
		/// Class to do the end of uninstall tidying up
		/// </summary>
		internal class CustomActionsUninstall
		{
			#region Externals
			[DllImport("user32.dll")]
			public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszWindow, string lpszClass); 
			[DllImport("user32.dll")]
			public static extern int PostMessage(int hWnd, uint Msg, long wParam, long lParam); 
			#endregion

			#region Class Fields
			private const string  MAIN_FORM_NAME = "UK Weather - Main Form Loop";
			private const string  REG_RUN     = @"Software\Microsoft\Windows\CurrentVersion\Run";
			private const uint    WM_CLOSE    = 16;
			private const uint    WM_DESTROY  = 2;
			#endregion

			#region Constructor
			internal CustomActionsUninstall()
			{
			}
      #endregion

			#region Main
			static void Main()
			{
				// Tidy up code goes here

				// 1. Close the window
				try
				{
					int hWnd = FindWindowEx(0, 0, null, MAIN_FORM_NAME);
					if(hWnd != 0)
					{
						int retval = PostMessage(hWnd, WM_CLOSE, 0, 0);
					}
				}
				catch {}

				// 2. Remove the runtime registry key if it exists
				try
				{
					RegistryKey rk = Registry.CurrentUser.OpenSubKey(REG_RUN, true);
					rk.DeleteValue("UKWeather", false);
				}
				catch {}
			}
			#endregion
		}
	}
}