using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw
{
	/// <summary>
	/// Software Updater Class
	/// </summary>
	internal static class Updater
	{
		private const string _defaultVersion = "1.0.0";

		private static readonly RegistryKey _registryVersionHive = Registry.LocalMachine;
		private const string _registryVersionKeyPath = "Software\\DotCastle\\DC.Crayon.Wlw";
		private const string _registryVersionValueName = "Version";
		private const string _registryPreReleaseValueName = "PreRelease";
		private const string _registryLocationValueName = "Location";

		private const string _gitReleaseListUrl = "https://api.github.com/repos/dotcastle/crayon-syntax-snippet-wlw/releases";
		private const string _gitSetupAssetName = "DC.Crayon.Wlw.Setup.zip";
		private const string _gitSetupMsiAssetName = "DC.Crayon.Wlw.Setup.msi";

		public const string CheckUpdatesOnStartupOptionName = "Updates_CheckOnStarup";
		public const string IncludePreReleaseVersionsOptionName = "Updates_IncludePreRelease";
		public const string LastUpdateCheckDateOptionName = "Updates_LastCheckDate";

		#region Versions
		private static VersionInfo _installedVersion;
		/// <summary>
		/// Retrieves the currently installed version
		/// </summary>
		public static VersionInfo InstalledVersion
		{
			get
			{
				if (_installedVersion == null)
				{
					_installedVersion = GetInstalledVersion();
				}
				return _installedVersion;
			}
		}

		/// <summary>
		/// Retrieves the installed version from the registry key
		/// </summary>
		/// <returns>Installed version</returns>
		private static VersionInfo GetInstalledVersion()
		{
			try
			{
				using (RegistryKey regKey = _registryVersionHive.OpenSubKey(_registryVersionKeyPath, RegistryKeyPermissionCheck.Default,
						RegistryRights.QueryValues))
				{
					var verValue = regKey.GetValue(_registryVersionValueName, _defaultVersion, RegistryValueOptions.None) as string;
					var preReleaseValue = (int)regKey.GetValue(_registryPreReleaseValueName, 0, RegistryValueOptions.None);
					var locationValue = regKey.GetValue(_registryLocationValueName, null, RegistryValueOptions.None) as string;

					if (verValue != null)
					{
						return new VersionInfo()
						{
							Version = new Version(verValue),
							PreRelease = (preReleaseValue != 0),
							InstallFolder = locationValue
						};
					}
				}
			}
			catch { }
			return new VersionInfo()
			{
				Version = new Version(_defaultVersion),
				PreRelease = false
			};
		}

		/// <summary>
		/// Retrieves the latest version from the Github repository
		/// </summary>
		/// <param name="includePreReleaseVersions">Include pre-release versions as well</param>
		/// <returns>Latest version</returns>
		public static VersionInfo GetLatestVersion(bool includePreReleaseVersions)
		{
			try
			{
				using (var webClient = new DownloadWebClient(Utils.CookieContainer))
				{
					// Properties
					webClient.UserAgent = Utils.UserAgent;

					// Download
					var jsonResponse = webClient.DownloadString(_gitReleaseListUrl);
					var releases = JsonConvert.DeserializeObject<JObject[]>(jsonResponse);

					// Find a suitable release
					string releaseVersion = null;
					bool isPreRelease = false;
					string assetUrl = null;
					string assetContentType = "application/octet-stream";
					var latestRelease = releases.FirstOrDefault(r =>
					{
						// Check Pre-release flag and include only if requested
						var preReleaseProperty = r.Property("prerelease");
						var preRelease = (preReleaseProperty != null) && (preReleaseProperty.Value != null) && (preReleaseProperty.Value.Type == JTokenType.Boolean) &&
						                 preReleaseProperty.Value.Value<bool>();
						if (preRelease && !includePreReleaseVersions)
						{
							return false;
						}
						isPreRelease = preRelease;

						// Version
						var tagNameProperty = r.Property("tag_name");
						if ((tagNameProperty != null) && (tagNameProperty.Value != null) && (tagNameProperty.Value.Type == JTokenType.String))
						{
							var tagName = (tagNameProperty.Value.Value<string>() ?? string.Empty).Trim();
							if (tagName.Length > 0)
							{
								releaseVersion = tagName;
							}
							else
							{
								return false;
							}
						}
						else
						{
							return false;
						}

						// Get the asset url
						var assetsProperty = r.Property("assets");
						if ((assetsProperty != null) && (assetsProperty.Value != null) && (assetsProperty.Value.Type == JTokenType.Array))
						{
							var assets = assetsProperty.Value.Value<JArray>();
							var latestAsset = assets.FirstOrDefault(a =>
							{
								var aO = a.ToObject<JObject>();

								// Get & check name
								var nameProperty = aO.Property("name");
								if ((nameProperty == null) || (nameProperty.Value == null) || (nameProperty.Value.Type != JTokenType.String))
								{
									return false;
								}
								var name = (nameProperty.Value.Value<string>() ?? string.Empty).Trim();
								if (!string.Equals(name, _gitSetupAssetName, StringComparison.OrdinalIgnoreCase))
								{
									return false;
								}

								// Get Content Type
								var contentTypeProperty = aO.Property("content_type");
								if ((contentTypeProperty != null) && (contentTypeProperty.Value != null) && (contentTypeProperty.Value.Type == JTokenType.String))
								{
									var contentType = (contentTypeProperty.Value.Value<string>() ?? string.Empty).Trim();
									if (contentType.Length > 0)
									{
										assetContentType = contentType;
									}
								}

								// Url
								var urlProperty = aO.Property("url");
								if ((urlProperty != null) && (urlProperty.Value != null) && (urlProperty.Value.Type == JTokenType.String))
								{
									var url = (urlProperty.Value.Value<string>() ?? string.Empty).Trim();
									if (url.Length > 0)
									{
										assetUrl = url;
										return true;
									}
								}
								return false;
							});
							if (latestAsset == null)
							{
								return false;
							}
						}
						else
						{
							return false;
						}

						// Match
						return true;
					});
					if (latestRelease != null)
					{
						return new VersionInfo()
						{
							Version = new Version(releaseVersion),
							PreRelease = isPreRelease,
							DownloadUrl = assetUrl,
							DownloadContentType = assetContentType
						};
					}
				}
			}
			catch { }
			return null;
		}
		#endregion

		#region Check
		/// <summary>
		/// Check on startup
		/// </summary>
		/// <param name="properties">Plugin properties</param>
		/// <param name="ownerWindow">Owner window if any</param>
		public static void Check(IProperties properties, IWin32Window ownerWindow = null)
		{
			bool includePreReleaseVersions = properties.GetBoolean(IncludePreReleaseVersionsOptionName, false);
			VersionInfo latestVersion = GetLatestVersion(includePreReleaseVersions);
			string currentDateTime = DateTime.UtcNow.ToLocalTime().ToString("F", CultureInfo.CurrentCulture);
			properties.SetString(LastUpdateCheckDateOptionName, currentDateTime);

			// Check if latest version is more than installed version
			if (latestVersion == null)
			{
				if (ownerWindow != null)
				{
					MessageBox.Show(ownerWindow, Resources.Error_UpdateCheckFailure, Resources.Label_Warning);
				}
			}
			else if (latestVersion.Version > InstalledVersion.Version)
			{
				if (DialogResult.OK == MessageBox.Show(ownerWindow, string.Format(CultureInfo.CurrentCulture, Resources.Text_LatestVersionAvailableDownloadPrompt_Version,
								latestVersion.Version),
							Resources.Label_Information, MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
							MessageBoxDefaultButton.Button1))
				{
					using (var updateForm = new DownloadUpdateForm(latestVersion))
					{
						DialogResult result = updateForm.ShowDialog(ownerWindow);
						if ((result == DialogResult.OK)
							&& updateForm.Downloaded)
						{
							Process.Start(new ProcessStartInfo(updateForm.DownloadedSetupFile));
						}
					}
				}
			}
			else
			{
				if (ownerWindow != null)
				{
					MessageBox.Show(ownerWindow, string.Format(CultureInfo.CurrentCulture, Resources.Text_InstalledVersionUpToDate_Version, InstalledVersion.Version), Resources.Label_Information);
				}
			}
		}
		#endregion
	}
}
