using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Forms
{
	/// <summary>
	/// Download update form
	/// </summary>
	internal partial class DownloadUpdateForm : Form
	{
		private readonly VersionInfo _versionInfo;
		private DownloadWebClient _webClient;

		#region Initialization & Disposal
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="versionInfo">Version info</param>
		public DownloadUpdateForm(VersionInfo versionInfo)
		{
			_versionInfo = versionInfo;
			InitializeComponent();
		}

		/// <summary>
		/// On Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClosed(object sender, FormClosedEventArgs e)
		{
			if (_webClient != null)
			{
				_webClient.Dispose();
				_webClient = null;
			}
			if (DialogResult != DialogResult.OK)
			{
				// Delete the file
				if ((DownloadedFile != null) && File.Exists(DownloadedFile))
				{
					try
					{
						File.Delete(DownloadedFile);
					}
					catch { }
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Full path of the downloaded file
		/// </summary>
		public string DownloadedFile
		{
			get;
			private set;
		}

		/// <summary>
		/// Whether successfully downloaded
		/// </summary>
		public bool Downloaded
		{
			get;
			private set;
		}
		#endregion

		#region Load & Closing Handlers
		/// <summary>
		/// Overridden to localize and start the download process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLoad(object sender, EventArgs e)
		{
			// Localize
			Text = Resources.Title_DownloadUpdateForm;
			messageLabel.Text = string.Format(CultureInfo.CurrentCulture, Resources.Text_DownloadProgressMessageFormat_Version,
				_versionInfo.Version);
			cancelButton.Text = Resources.Label_Cancel;

			// Initialize this dialog result
			Downloaded = false;

			// Initiate download, create a temp file
			DownloadedFile = Path.GetTempFileName();
			if (File.Exists(DownloadedFile))
			{
				File.Delete(DownloadedFile);
			}
			DownloadedFile += ".msi";
			if (File.Exists(DownloadedFile))
			{
				File.Delete(DownloadedFile);
			}

			// Start download to file
			_webClient = new DownloadWebClient(Utils.CookieContainer);
			_webClient.UserAgent = Utils.UserAgent;
			_webClient.Accept = _versionInfo.DownloadContentType;
			progressBar.Maximum = 1000000;
			_webClient.DownloadProgressChanged += this.OnDownloadProgress;
			_webClient.DownloadFileCompleted += this.OnDownloadCompleted;
			_webClient.DownloadFileAsync(new Uri(_versionInfo.DownloadUrl, UriKind.Absolute), DownloadedFile);
		}

		private void OnClose(object sender, EventArgs e)
		{
			DialogResult = cancelButton.DialogResult;
			Close();
		}

		/// <summary>
		/// Download progress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
		{
			// Check if invoke required
			if (progressBar.InvokeRequired)
			{
				progressBar.Invoke(new DownloadProgressChangedEventHandler(OnDownloadProgress), sender, e);
				return;
			}

			// Update data
			if (e.TotalBytesToReceive > 0)
			{
				progressBar.Value = (int)(e.BytesReceived * progressBar.Maximum / e.TotalBytesToReceive);
			}
			else
			{
				progressBar.Style = ProgressBarStyle.Marquee;
				var progressText = string.Format(CultureInfo.CurrentCulture, Resources.Text_ProgressMessage_Received_ToReceive,
					e.BytesReceived, e.TotalBytesToReceive);
				messageLabel.Text = string.Format(CultureInfo.CurrentCulture, Resources.Text_DownloadProgressMessageFormat_Version + " ({1})",
					_versionInfo.Version, progressText);
			}

			// Update
			progressBar.Invalidate();
		}

		private void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			// Check if invoke required
			if (progressBar.InvokeRequired)
			{
				progressBar.Invoke(new AsyncCompletedEventHandler(OnDownloadCompleted), sender, e);
				return;
			}

			if (e.Error != null)
			{
				// Show error
				MessageBox.Show(this,
					string.Format(CultureInfo.CurrentCulture, Resources.Error_DownloadError_Error, e.Error.Message),
					Resources.Label_Error);
			}
			if ((e.Error != null) || e.Cancelled)
			{
				// Delete File
				if ((DownloadedFile != null)
					&& File.Exists(DownloadedFile))
				{
					try
					{
						File.Delete(DownloadedFile);
						DownloadedFile = null;
					}
					catch { }
				}
			}
			else
			{
				Downloaded = true;
			}

			// Close
			messageLabel.Text = string.Format(CultureInfo.CurrentCulture, Resources.Text_DownloadProgressCompletedMessageFormat_Version,
				_versionInfo.Version);
			cancelButton.Text = Resources.Label_Close;
			cancelButton.DialogResult = DialogResult.OK;

			// Close this dialog
			DialogResult = DialogResult.OK;
			Close();
		}

		/// <summary>
		/// Overridden to confirm closure if closing without completion
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnClosing(object sender, FormClosingEventArgs e)
		{
			// Verify if we are canceling and ask for confirmation
			if (DialogResult != DialogResult.OK)
			{
				if (DialogResult.No ==
				    MessageBox.Show(this, Resources.Text_DownloadUpdateCancelConfirmation, Resources.Label_Warning,
					    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
				{
					e.Cancel = true;
				}
				else
				{
					try
					{
						_webClient.CancelAsync();
					}
					catch { }
				}
			}
		}
		#endregion
	}
}
