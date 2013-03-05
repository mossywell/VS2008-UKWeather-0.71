using System;
using System.Diagnostics;

namespace Mossywell.UKWeather
{
	internal class DataUpdater
	{
		#region Class Fields
		private string                      _strPostcode;
		private TemperatureScales           _temperaturescale;
		private string                      _strUrl;
		private NotifyIconParameters        _nip;
		private DataUpdaterCallbackDelegate _ducd;
		#endregion

		#region Constructor
		public DataUpdater(string postcode, TemperatureScales temperaturescale, string url, NotifyIconParameters nip, DataUpdaterCallbackDelegate ducd)
		{
			_strPostcode = postcode;
			_temperaturescale = temperaturescale;
			_strUrl = url;
			_nip = nip;
			_ducd = ducd;
		}
    #endregion

		#region Utility Methods
		internal void DoTheUpdate()
		{
			// Do the main stuff here
			WebParser wp                 = new WebParser();
			string strNotifyIconText     = _nip.LastIconText;
			string strNotifyIconTemp     = _nip.LastIconTemp;
			_nip.NotifyIconTempHasChanged = false;

			wp.GetAndParseURL(_strUrl, _strPostcode);
			_nip.LastIconTimeString.UpdateCurrentTime(); // We do this in all cases
			switch(wp.Status)
			{
				case WebParserStatus.OK:
					_nip.PostcodeChangedSinceNoNetwork = false;
					_nip.NoNetworkHasBeenLogged        = false;
					if(_temperaturescale == TemperatureScales.Celsius)
					{
						strNotifyIconText = wp.NotifyIconTextCelsius;
						strNotifyIconTemp = wp.TempCelsiusNow;
					}
					else
					{
						strNotifyIconText = wp.NotifyIconTextFarenheit;
						strNotifyIconTemp = wp.TempFarenheitNow;
					}
					_nip.LastIconTimeString.UpdateLastSuccessfulTime();
					break;

				case WebParserStatus.BadPostcode:
					_nip.PostcodeChangedSinceNoNetwork = false;
					_nip.NoNetworkHasBeenLogged        = false;
					strNotifyIconText                  = _strPostcode + ": Invalid postcode?";
					strNotifyIconTemp                  = Constants.CHAR_BADPOSTCODE;
					break;

				case WebParserStatus.NoNetwork:
					if(_nip.PostcodeChangedSinceNoNetwork == false && _strPostcode != _nip.LastPostcode)
						_nip.PostcodeChangedSinceNoNetwork = true;
					if(_nip.PostcodeChangedSinceNoNetwork)
					{
						// FYI, We will have already logged this in the Event Log
						strNotifyIconText = _strPostcode + ": No network connection? See Event Log.";
						strNotifyIconTemp = Constants.CHAR_NONETWORK;
					}
					else
					{
						if(!_nip.NoNetworkHasBeenLogged)
						{
							// Log it once only
							string strError;
							strError = "The URL was:"                       + Environment.NewLine;
							strError += _strUrl + _strPostcode              + Environment.NewLine;
							strError += "The unparsed returned string was:" + Environment.NewLine;
							strError += wp.RawWebResponse                   + Environment.NewLine;
							strError += "The error returned was:"           + Environment.NewLine;
							strError += wp.Error                            + Environment.NewLine;
							FormMain.SendToEventLog(strError, EventLogEntryType.Warning);
							_nip.NoNetworkHasBeenLogged = true;
						}
					}

					if(_nip.LastStatus == WebParserStatus.BadPostcode)
					{
						// FYI, We will have already logged this in the Event Log
						strNotifyIconText = _strPostcode + ": No network connection? See Event Log.";
						strNotifyIconTemp = Constants.CHAR_NONETWORK;
					}
					else
					{
						// Don't change the icon or text
					}
					break;
			}
      
			// Create the new text etc.
			if(_nip.LastIconText != strNotifyIconText) _nip.NotifyIconTextHasChanged = true;
			if(_nip.LastIconTemp != strNotifyIconTemp) _nip.NotifyIconTempHasChanged = true;
			
			_nip.CompleteNotifyIconText        = CreateNotifyIconText(strNotifyIconText);
			_nip.LastStatus                    = wp.Status;
			_nip.LastPostcode                  = _strPostcode;
			_nip.LastIconText                  = strNotifyIconText;
			_nip.LastIconTemp                  = strNotifyIconTemp;

			// Tidy up
			wp = null;

			// Can't use return values, so use the callback to send the NotifyIconParameters back to the UI
			_ducd(_nip);
		}

		private string CreateNotifyIconText(string message)
		{
			string strOutput;

			strOutput = _nip.LastIconTimeString.WholeTimeString + Environment.NewLine + message;
			// This shouldn't be needed as the web parser already truncates the text,
			// but we'll keep it in to be safe as text too long causes an exception.
			if(strOutput.Length > 63)
			{
				strOutput = _nip.LastIconTimeString.WholeTimeString + Environment.NewLine + "Output too long to display!";
			}
			return strOutput;
		}
		#endregion
	}
}
