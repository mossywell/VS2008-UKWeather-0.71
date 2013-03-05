using System;
using System.Net; // Webrequest etc.
using System.IO; // Stream

namespace Mossywell.UKWeather
{
	/// <summary>
	/// A set of static utlities relating to the Internet.
	/// </summary>
	public class WebUtils
	{
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public WebUtils()
		{
		}
		#endregion

		#region Static Public Methods
		/// <summary>
		/// Gets a URI and returns the response as a string. Note that:
		/// 1. The caller must handle any exceptions, which generally occur when the
		///    request is being got and when the stream reader is reading the response.
		/// 2. Keepalives are set to false.
		/// 3. If a redirection took place (that is, the URI requested was not the one
		///    that was returned), an empty string is returned.
		/// </summary>
		/// <param name="requestUriString">The URI that identifies the Internet resource</param>
		/// <param name="timeout">The length of time, in milliseconds, until the
		/// request times out, or the value Timeout.Infinite to indicate that the request
		/// does not time out.</param>
		/// <returns></returns>
		public static string GetURI(string requestUriString, int timeout)
		{
			// Method variables
			HttpWebRequest  request  = null;
			HttpWebResponse response = null;
			string strResponse = "";

			// Check the arguments and throw an exception if invalid
			if(requestUriString == "")
			{
				throw new ArgumentException("The URI cannot be an empty string");
			}

			// Set the request to the URL that was passed to the method
			request = (HttpWebRequest)WebRequest.Create(requestUriString);
			// request.KeepAlive = false; HTTP 1.1 makes this pretty much obsolete
			request.Timeout = timeout;

			// OK, try to grab the data. This can generate an exception.
			response = (HttpWebResponse)request.GetResponse();

			// Only process the data if the response isn't still null
			if(response != null)
			{
				string strResponseUrl = response.ResponseUri.AbsoluteUri;
				if(strResponseUrl == requestUriString)
				{
					// Read from the data stream
					Stream stream = response.GetResponseStream();
					StreamReader reader = new StreamReader(stream);

					// This can throw an exception.
					strResponse  = reader.ReadToEnd();

					// Tidy up
					reader.Close();
					stream.Close();
				}
			}

            response.Close();
			return strResponse;
		}
		#endregion
	}
}
