using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Framework;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Forms
{
	public partial class ContentForm : Form
	{
		protected IProperties _contentProperties;
		protected IProperties _properties;

		#region Initialization
		public ContentForm(IProperties contentProperties, IProperties properties)
		{
			_contentProperties = contentProperties;
			_properties = properties;
			InitializeComponent();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			// Localize
			Text = new ResourceManager(typeof(CrayonSnippetPlugin)).GetString("WriterPlugin.Name") ?? Text;
			okButton.Text = Resources.Label_OK;
			cancelButton.Text = Resources.Label_Cancel;
			resetPluginButton.Text = Resources.Label_Reset;

			// Initialize
			FormHelper.InitializeOptionsTable(settingsTable, false);
			FormHelper.BuildOptionsTable(settingsTable, false, toolTip, errorProvider, _contentProperties, _properties);

			// Inline & Url controls
			CheckBox inlineCheckbox = settingsTable.Controls.OfType<CheckBox>()
				.SingleOrDefault(c => c.Tag == Options.ContentOnlyOptions.Single(o => string.Equals(o.Name, "Inline", StringComparison.Ordinal)));
			TextBox urlTextBox = settingsTable.Controls.OfType<TextBox>()
				.SingleOrDefault(c => c.Tag == Options.ContentOnlyOptions.Single(o => string.Equals(o.Name, "Url", StringComparison.Ordinal)));
			urlTextBox.Enabled = !inlineCheckbox.Checked;
			inlineCheckbox.CheckedChanged += (s, pe) =>
			{
				urlTextBox.Enabled = !inlineCheckbox.Checked;
			};
		}
		#endregion

		#region Event Handlers
		private void OnReset(object sender, EventArgs e)
		{
			FormHelper.InitializeOptionsTable(settingsTable, false);
			FormHelper.BuildOptionsTable(settingsTable, false, toolTip, errorProvider, null, _properties);
		}

		private void OnSave(object sender, EventArgs e)
		{
			if (ValidateChildren())
			{
				FormHelper.SaveOptions(settingsTable, false, _contentProperties);
				DialogResult = DialogResult.OK;
				Close();
			}
		}
		#endregion
	}
}
