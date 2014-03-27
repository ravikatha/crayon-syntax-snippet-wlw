using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DC.Crayon.Wlw
{
	/// <summary>
	/// Internal version info structure
	/// </summary>
	internal class VersionInfo
	{
		#region Properties
		/// <summary>
		/// Version Information
		/// </summary>
		public Version Version
		{
			get;
			set;
		}

		/// <summary>
		/// Is Pre-release version
		/// </summary>
		public bool PreRelease
		{
			get;
			set;
		}

		/// <summary>
		/// Download url
		/// </summary>
		public string DownloadUrl
		{
			get;
			set;
		}

		/// <summary>
		/// Content type of the download
		/// </summary>
		public string DownloadContentType
		{
			get;
			set;
		}
		#endregion
	}
}
