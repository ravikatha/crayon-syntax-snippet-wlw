using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Framework;
using DC.Crayon.Wlw.Properties;
using System.Resources;

namespace DC.Crayon.Wlw.Forms
{
	public partial class OptionsForm : Form
	{
		protected IProperties _properties;
		private string _productNameLabel;

		#region Initialization
		public OptionsForm(IProperties properties)
		{
			_properties = properties;
			InitializeComponent();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			// Localize
			Text = Resources.Title_OptionsForm;
			okButton.Text = Resources.Label_OK;
			cancelButton.Text = Resources.Label_Cancel;
			resetPluginButton.Text = Resources.Label_Reset;

			pluginTabPage.Text = Resources.Label_PlugIn;
			aboutTabPage.Text = Resources.Label_About;

			updateManagementGroupBox.Text = Resources.Label_UpdateManagement;
			checkForUpdatesCheckbox.Text = Resources.Label_CheckForUpdates;
			includePreReleaseVersionsCheckbox.Text = Resources.Label_IncludePreReleaseVersions;
			lastCheckedAtFieldLabel.Text = Resources.Label_LastCheckedAt;
			checkNowButton.Text = Resources.Label_CheckNow;

			// Update product name
			if (_productNameLabel == null)
			{
				_productNameLabel = productNameLabel.Text;
			}
			productNameLabel.Text = _productNameLabel + " v" + Updater.InstalledVersion.Version.ToString(3);

			// Initialize
			FormHelper.InitializeOptionsTable(settingsTable, true);
			FormHelper.BuildOptionsTable(settingsTable, true, toolTip, errorProvider, _properties, _properties);

			// Other fields
			checkForUpdatesCheckbox.Checked = _properties.GetBoolean(Updater.CheckUpdatesOnStartupOptionName, true);
			includePreReleaseVersionsCheckbox.Checked = _properties.GetBoolean(Updater.IncludePreReleaseVersionsOptionName, false);
			lastCheckedAtLabel.Text = _properties.GetString(Updater.LastUpdateCheckDateOptionName, string.Empty);
		}
		#endregion

		#region Event Handlers
		private void OnReset(object sender, EventArgs e)
		{
			// Reset
			FormHelper.InitializeOptionsTable(settingsTable, true);
			FormHelper.BuildOptionsTable(settingsTable, true, toolTip, errorProvider, null, _properties);

			// Other fields
			checkForUpdatesCheckbox.Checked = true;
			includePreReleaseVersionsCheckbox.Checked = false;
		}

		private void OnSave(object sender, EventArgs e)
		{
			if (ValidateChildren())
			{
				// Save Table
				FormHelper.SaveOptions(settingsTable, true, _properties);

				// Other properties
				_properties.SetBoolean(Updater.CheckUpdatesOnStartupOptionName, checkForUpdatesCheckbox.Checked);
				_properties.SetBoolean(Updater.IncludePreReleaseVersionsOptionName, includePreReleaseVersionsCheckbox.Checked);

				// Close
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		/// <summary>
		/// Check for updates
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCheckUpdates(object sender, EventArgs e)
		{
			Updater.Check(_properties, this);
			lastCheckedAtLabel.Text = _properties.GetString(Updater.LastUpdateCheckDateOptionName, string.Empty);
		}
		#endregion

		#region Link Label Handlers
		private void OnBlogClick(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("http://www.dotcastle.com/blog"));
		}

		private void OnWebClick(object sender, EventArgs e)
		{
			Process.Start(new ProcessStartInfo("http://www.dotcastle.com"));
		}
		#endregion
	}
}
