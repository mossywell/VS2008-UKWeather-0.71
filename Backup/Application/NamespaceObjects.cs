using System;
using System.Drawing;        // Color, Icon
using System.ComponentModel; // Description

namespace Mossywell.UKWeather
{
	#region Namespace Delegates
	delegate void DataUpdaterCallbackDelegate(NotifyIconParameters nips);
	#endregion

	#region Namespace Structures
	internal struct Constants
	{
		internal const string            MAIN_FORM_NAME             = "UK Weather - Main Form Loop";

		// User option defaults in whatever types is most convenient!
		internal const int               DEFAULT_CHECKVERANDURL     = 1;
		internal const IconChangeOptions DEFAULT_ICONCHANGE         = IconChangeOptions.Flash;
		internal const TemperatureScales DEFAULT_TEMPERATURESCALE   = TemperatureScales.Celsius;
		internal const int               DEFAULT_ICONFLASHFREQUENCY = 500; // milliseconds
		internal const int               DEFAULT_NEWVERSIONPOPUP    = 1;
		internal const string            DEFAULT_POSTCODE           = "SW19";
		internal const bool              DEFAULT_RUNATSYSTEMSTARTUP = false;
		internal const int               DEFAULT_UPDATEINTERVAL     = 300;   // seconds

		internal const LogLevel          DEFAULT_LOGLEVEL           = LogLevel.None;
		internal const int               DEFAULT_WEBPARSERTIMEOUT   = 20000; // milliseconds
		internal const int               DEFAULT_UKWDATATIMEOUT     = 5000; // milliseconds

		internal const float             ICON_TEXT_WIDTH_MAX        = 17f;
		internal const float             FONT_SIZE_MIN              = 5f;

		internal const int               COLOUR_WINDOW_WIDTH_C      = 232;
		internal const int               COLOUR_WINDOW_WIDTH_F      = 352;

		internal const int THREAD_SLEEP_NO_ERRORS = 1000;
		internal const int THREAD_SLEEP_ERRORS    = 3000;

		internal const string REGEX_VERSION = @"^Version=(\d+\.\d+\.\d+\.\d+)";
		internal const string REGEX_URL     = @"^URL=(\S+)";

		internal const string CHAR_BADPOSTCODE   = "P";
		internal const string CHAR_NONETWORK     = "N";
		internal const string CHAR_OBTAININGDATA = "?";
		internal const string CHAR_ODDDATA       = "X";

		internal const int    MIN_UPDATEINTERVAL     = 30;    // seconds
		internal const int    MAX_UPDATEINTERVAL     = 14400; // seconds
		internal const int    MIN_ICONFLASHFREQUENCY = 100;   // milliseconds
		internal const int    MAX_ICONFLASHFREQUENCY = 5000;  // milliseconds

		internal const string URL_DATA               = @"http://uk.weather.com/weather/local/";
		internal const string URL_UKW_DOWNLOAD       = @"http://www.mossywell.com/downloads/#UKWeather";
		internal const string URL_UKW_INFO           = @"http://www.mossywell.com/downloads/UKWeatherInfo.txt";

		internal const string REG_RUN                = @"Software\Microsoft\Windows\CurrentVersion\Run";
		internal const string REG_PARAMS             = @"Software\Mossywell\UK Weather";
		internal const string REG_LOG                = "Log";

    internal const string REG_CHECKVERANDURL     = "CheckVersionAndUrl";
		internal const string REG_ICONCHANGE         = "IconChangeOption";
		internal const string REG_TEMPERATURESCALE   = "TemperatureScale";
		internal const string REG_ICONFLASHFREQUENCY = "IconFlashFrequency";
		internal const string REG_POSTCODE           = "Postcode";
		internal const string REG_UPDATEINTERVAL     = "UpdateInterval";
		internal const string REG_NEWVERSIONPOPUP    = "NewVersionPopup";
	}

	struct ColorOnOff
	{
		internal Color OnColor;
		internal Color OffColor;
	}

	internal struct IconPair
	{
		internal Icon OnIcon;
		internal Icon OffIcon;
	}

	internal struct NotifyIconParameters
	{
		internal TimeString      LastIconTimeString;
		internal string          LastIconText;
		internal string          LastIconTemp;
		internal WebParserStatus LastStatus;
		internal string          LastPostcode;
		internal bool            PostcodeChangedSinceNoNetwork;
		internal bool            NoNetworkHasBeenLogged;
		internal string          CompleteNotifyIconText;
		internal bool            NotifyIconTextHasChanged;
		internal bool            NotifyIconTempHasChanged;
	}

	internal struct UserOptions
	{
		// This struct stores the values in their "real" types rather
		// than registry types
		internal bool              CheckVersionAndUrl;
		internal IconChangeOptions IconChangeOption;
		internal TemperatureScales TemperatureScale;
		internal int               IconFlashFrequency;
		internal bool              NewVersionPopup;
		internal string            Postcode;
		internal bool              RunAtSystemStartup;
		internal int               UpdateInterval;
	}
	#endregion

	#region Namespace Enums
	internal enum TemperatureScales
	{
		Celsius,
		Farenheit,
	}

	internal enum LogLevel
	{
		None,        // 0
		Error,       // 1
		Warning,     // 2
		Information, // 3
		KitchenSink, // 4
	}

	internal enum IconChangeOptions
	{
		[Description("THIS MUST ALWAYS BE THE FIRST VALUE")]        FirstValue,
		[Description("No Change")]          DoNothing,
		[Description("Change Once")]        ChangeColour,
		[Description("Flash")]              Flash,
		[Description("Flash Continuously")] FlashContinuously,
		[Description("THIS MUST ALWAYS BE THE LAST VALUE")]         LastValue,
	}

	internal enum WebParserStatus
	{
		OK,
		NoNetwork,
		BadPostcode,
		GeneralError,
	}

	internal enum HelpFile
	{
		[Description("THIS MUST ALWAYS BE THE FIRST VALUE")] FirstValue,
		[Description("running-about.htm")]                   FormAbout,
		[Description("running-colourscheme.htm")]            FormColours,
		[Description("running-options.htm")]                 FormOptions,
		[Description("running-desktop.htm")]                 Desktop,
		[Description("options-updateinterval.htm")]          OptionsUpdateInterval,
		[Description("options-postcode.htm")]                OptionsPostcode,
		[Description("options-changes.htm")]                 OptionsWhenChanges,
		[Description("options-runatstartup.htm")]            OptionsStartup,
		[Description("options-notifyuser.htm")]              OptionsNotifyUser,
		[Description("options-newvercheck.htm")]             OptionsCheckVerAndUrl,
		[Description("options-flashfrequency.htm")]          OptionsFlashFrequency,
		[Description("THIS MUST ALWAYS BE THE LAST VALUE")]  LastValue,
	}
	#endregion
}
