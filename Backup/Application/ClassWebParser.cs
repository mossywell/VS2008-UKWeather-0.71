using System;
using System.Text.RegularExpressions; // Regex

namespace Mossywell.UKWeather
{
	internal class WebParser
	{
		#region Class Fields
		private string          _strRawWebResponse;
		private string          _strError;
		private string          _strWorkingData;
		private WebParserStatus _wpStatus;
		private string          _strNotifyIconTextCelsius;
		private string          _strNotifyIconTextFarenheit;
		private string          _strTempCelsiusNow;
		private string          _strTempFarenheitNow;
		private string          DEG = (char)0186 + "";
		#endregion

		#region Constructor
		internal WebParser()
		{
			_strRawWebResponse = "";
			_strError = "";
			_strWorkingData = "";
			_wpStatus = WebParserStatus.GeneralError;
			_strNotifyIconTextCelsius = "";
			_strNotifyIconTextFarenheit = "";
		}
		#endregion

		#region Methods
		internal void GetAndParseURL(string url, string postcode)
		{
			// Method variables
			string strTempFLC = "";
			string strTempFLF = "";
			string strDesc    = "";
			string strThisRow = "";

			url += postcode + "";

			try
			{
				_strWorkingData = Mossywell.WebUtils.GetURI(url, Constants.DEFAULT_WEBPARSERTIMEOUT);
				_strRawWebResponse = _strWorkingData;
        _strError = "";
			}
			catch(Exception ex)
			{
				_strWorkingData    = "";
				_strRawWebResponse = "";
				_strError = ex.Message;
			}

			if(_strRawWebResponse == "")
			{
				_strRawWebResponse = "<Empty>";
				_wpStatus = WebParserStatus.NoNetwork;
				return;
			}

			// Start to parse the data obtained
			TrimLeftToThisString("weather info");
			if(_strWorkingData == "")
			{
				_wpStatus = WebParserStatus.BadPostcode;
				return;
			}

			// Some serious chopping up of the data!
			TrimToThisHTMLBlock("TABLE");
			
			// Description
			strDesc = new Regex("  ").Replace(GetRowText(3), " ");
			if(strDesc == "")
			{
				strDesc = Constants.CHAR_ODDDATA;
			}
			if(strDesc.Length > 29) strDesc = strDesc.Substring(0, 29);
				
			// Temperature now in degrees C and F excluding degree symbols
			strThisRow = GetRowText(5);
			_strTempCelsiusNow = new Regex(@"^([^,]*),?([^,]*)&deg;.*").Match(strThisRow).Result("$1$2");
			if(_strTempCelsiusNow == "")
			{
				_strTempCelsiusNow = Constants.CHAR_ODDDATA;
				_strTempFarenheitNow = Constants.CHAR_ODDDATA;
			}
			else
			{
				_strTempFarenheitNow = Convert.ToString(System.Math.Round(Convert.ToDouble(_strTempCelsiusNow) / 5.0 * 9.0 + 32.0));
			}

			// Feels like in C and F excluding degree symbols
			strThisRow = GetRowText(7);
			strTempFLC = new Regex(@"^Feels Like([^,]*),?([^,]*)&deg;.*").Match(strThisRow).Result("$1$2");
			if(strTempFLC == "") 
			{
				strTempFLC = Constants.CHAR_ODDDATA;
				strTempFLF = Constants.CHAR_ODDDATA;
			}
			else
			{
				strTempFLF = Convert.ToString(System.Math.Round(Convert.ToDouble(strTempFLC) / 5.0 * 9.0 + 32.0));
			}

			// Notify icon texts
			_strNotifyIconTextCelsius = postcode + ": " + strDesc + ", " + _strTempCelsiusNow + DEG + "C (" + strTempFLC + DEG + "C)";
			_strNotifyIconTextFarenheit = postcode + ": " + strDesc + ", " + _strTempFarenheitNow + DEG + "F (" + strTempFLF + DEG + "F)";
			
			// We made is this far, so must be OK!
			_wpStatus = WebParserStatus.OK;
		}

		private void TrimLeftToThisString(string search)
		{
			if(search == "") return;

			Regex re = new Regex(search + @".*", RegexOptions.Singleline);
			_strWorkingData = re.Match(_strWorkingData).ToString();
		}

		private void TrimToThisHTMLBlock(string tag)
		{		
			_strWorkingData = DoTagSearch(tag, 0);
		}

		private string DoTagSearch(string tag, int startposition)
		{
			Regex re;
			Match m = null;
			string str;

			// Truncate the string to start with the tag
			re = new Regex("<" + tag + ".*", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			str = re.Match(_strWorkingData, startposition).ToString();

			// Find all the searched for tags taking nesting into account
			re = new Regex("<" + tag + "|</" + tag, RegexOptions.Singleline | RegexOptions.IgnoreCase);
			int intPos    = 0;
			int intCount  = 0;
			bool blnFound = false;

			while(!blnFound)
			{
				m = re.Match(str, intPos);
				intPos = m.Index + 1;

				if(String.Compare(m.Value, "<" + tag, true) == 0)
				{
					intCount++;
				}
				else
				{
					intCount--;
					if(intCount == 0) blnFound = true;
				}
			}

			// Find the closing ">"
			re = new Regex(">", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			m = re.Match(str, intPos);

			return str.Substring(0, m.Index + 1);
		}

		private string GetThisHTMLBlock(string tag, string startfromthisstring)
		{
			Regex re;
			string str;

			re = new Regex(startfromthisstring, RegexOptions.Singleline | RegexOptions.IgnoreCase);
			str = DoTagSearch(tag, re.Match(_strWorkingData).Index + 1);

			// Final tidying up
			re = new Regex(@"^<[^>]*>(.*)<[^>]*>$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			str = re.Replace(str, "$1");

			return str;
		}

		private string GetRowText(int row)
		{
			Regex re;
			Match m;
			CaptureCollection cc;
			string str;

			// Find each row and save in captures
			re = new Regex(@"(?:.*?<TR>(.*?)</TR>){8,}.*", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			m = re.Match(_strWorkingData);

			// Grab the captures
			cc = m.Groups[1].Captures;
			str = cc[row].ToString();

			// Strip out remaining tags
			re = new Regex(@"<[^>]*>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			return re.Replace(str, "");
		}
		#endregion

		#region Properties
		internal string RawWebResponse
		{
			get
			{
				return _strRawWebResponse;
			}
		}

		internal string Error
		{
			get
			{
				return _strError;
			}
		}

		internal WebParserStatus Status
		{
			get
			{
				return _wpStatus;
			}
		}

		internal string NotifyIconTextCelsius
		{
			get
			{
				return _strNotifyIconTextCelsius;
			}
		}

		internal string NotifyIconTextFarenheit
		{
			get
			{
				return _strNotifyIconTextFarenheit;
			}
		}
				
		internal string TempCelsiusNow
		{
			get
			{
				return _strTempCelsiusNow;
			}
		}

		internal string TempFarenheitNow
		{
			get
			{
				return _strTempFarenheitNow;
			}
		}
		#endregion
	}
}
