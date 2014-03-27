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

		private string _installFolder;
		/// <summary>
		/// Install folder
		/// </summary>
		public string InstallFolder
		{
			get { return _installFolder; }
			set
			{
				var val = (value ?? string.Empty).Trim();
				_installFolder = (val.Length == 0) ? null : val;
			}
		}

		private string _downloadUrl;
		/// <summary>
		/// Download url
		/// </summary>
		public string DownloadUrl
		{
			get { return _downloadUrl; }
			set
			{
				var val = (value ?? string.Empty).Trim();
				_downloadUrl = (val.Length == 0) ? null : val;
			}
		}

		private string _downloadContentType;
		/// <summary>
		/// Content type of the download
		/// </summary>
		public string DownloadContentType
		{
			get { return _downloadContentType; }
			set
			{
				var val = (value ?? string.Empty).Trim();
				_downloadContentType = (val.Length == 0) ? null : val;
			}
		}
		#endregion
	}
}
