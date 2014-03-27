using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DC.Crayon.Wlw
{
	/// <summary>
	/// Override for cookie support & content type support
	/// </summary>
	internal class DownloadWebClient : WebClient
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cookieContainer">Cookie container</param>
		public DownloadWebClient(CookieContainer cookieContainer = null)
		{
			_cookieContainer = cookieContainer;
		}

		private readonly CookieContainer _cookieContainer;
		/// <summary>
		/// Cookie container
		/// </summary>
		public virtual CookieContainer CookieContainer
		{
			get { return _cookieContainer; }
		}

		private string _contentType;
		/// <summary>
		/// Content type of the request
		/// </summary>
		public virtual string ContentType
		{
			get { return _contentType; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_contentType = (val.Length == 0) ? null : val;
			}
		}

		private string _userAgent;
		/// <summary>
		/// User agent of the request
		/// </summary>
		public virtual string UserAgent
		{
			get { return _userAgent; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_userAgent = (val.Length == 0) ? null : val;
			}
		}

		private string _accept;
		/// <summary>
		/// Accept of the request
		/// </summary>
		public virtual string Accept
		{
			get { return _accept; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_accept = (val.Length == 0) ? null : val;
			}
		}

		/// <summary>
		/// Web request override
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		protected override WebRequest GetWebRequest(Uri address)
		{
			var webRequest = base.GetWebRequest(address);
			var httpWebRequest = webRequest as HttpWebRequest;

			// Properties of HttpWebRequest
			if (httpWebRequest != null)
			{
				// Set cookie container
				var cookieContainer = CookieContainer;
				if (cookieContainer != null)
				{
					httpWebRequest.CookieContainer = cookieContainer;
				}

				// Set content type
				var contentType = ContentType;
				if (contentType != null)
				{
					httpWebRequest.ContentType = contentType;
				}

				// Set User Agent
				var userAgent = UserAgent;
				if (userAgent != null)
				{
					httpWebRequest.UserAgent = userAgent;
				}

				// Set Accept
				var accept = Accept;
				if (accept != null)
				{
					httpWebRequest.Accept = accept;
				}
			}

			// Return
			return webRequest;
		}
	}
}
