using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

		#region Initialization
		public OptionsForm(IProperties properties)
		{
			_properties = properties;
			InitializeComponent();

			// Localize
			Text = Resources.Title_OptionsForm;
			okButton.Text = Resources.Label_OK;
			cancelButton.Text = Resources.Label_Cancel;
			resetPluginButton.Text = Resources.Label_Reset;

			// Initialize
			FormHelper.InitializeOptionsTable(settingsTable, true);
			FormHelper.BuildOptionsTable(settingsTable, true, toolTip, errorProvider, _properties, _properties);
		}
		#endregion

		#region Event Handlers
		private void OnReset(object sender, EventArgs e)
		{
			FormHelper.InitializeOptionsTable(settingsTable, true);
			FormHelper.BuildOptionsTable(settingsTable, true, toolTip, errorProvider, null, _properties);
		}

		private void OnSave(object sender, EventArgs e)
		{
			if (ValidateChildren())
			{
				FormHelper.SaveOptions(settingsTable, true, _properties);
				DialogResult = DialogResult.OK;
				Close();
			}
		}
		#endregion
	}
}
