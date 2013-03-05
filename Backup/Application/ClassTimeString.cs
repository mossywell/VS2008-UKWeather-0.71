using System;

namespace Mossywell.UKWeather
{
	internal class TimeString
	{
		#region Class Fields
		string _strLastSuccessfulTime;
		string _strCurrentTime;
		#endregion

		#region Constructor
		internal TimeString()
		{
			_strLastSuccessfulTime = "--:--";
			_strCurrentTime = "--:--";
		}
		#endregion

		#region Methods
		internal void UpdateCurrentTime()
		{
			_strCurrentTime = DateTime.Now.ToShortTimeString();
		}

		internal void UpdateLastSuccessfulTime()
		{
			_strLastSuccessfulTime = DateTime.Now.ToShortTimeString();
		}
		#endregion

		#region Properties
		internal string WholeTimeString
		{
			get
			{
				return _strCurrentTime + " (" + _strLastSuccessfulTime + ")";
			}
		}
		#endregion
	}
}
