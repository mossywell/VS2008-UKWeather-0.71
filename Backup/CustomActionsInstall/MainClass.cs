using System;

namespace Mossywell
{
	namespace UKWeather
	{
		/// <summary>
		/// Class to do the end of install stuff
		/// </summary>
		internal class CustomActionsInstall
		{
			#region Constructor
			public CustomActionsInstall()
			{
			}
			#endregion

			#region Main
			static void Main()
			{
				try
				{
					string str = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Mossywell\UK Weather\README.TXT";
					System.Diagnostics.Process.Start(str);
				}
				catch {}
			}
			#endregion
		}
	}
}
