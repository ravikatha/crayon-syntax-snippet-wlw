using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Forms;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class CustomOptionListOption : Option
	{
		#region Overrides
		/// <summary>
		/// Fills the combo box items
		/// </summary>
		/// <param name="comboBox"></param>
		/// <param name="selectedValue"></param>
		/// <param name="handleCustomTextChange"></param>
		/// <param name="customOptionsTextBox"></param>
		/// <param name="standardOptions"></param>
		/// <param name="customOption"></param>
		/// <param name="pluginProperties"></param>
		protected virtual void FillItems(ComboBox comboBox, string selectedValue, bool handleCustomTextChange, TextBox customOptionsTextBox, IEnumerable<ListItem> standardOptions, Option customOption, IProperties pluginProperties)
		{
			// Clear
			ListItem selectedItem = comboBox.SelectedItem as ListItem;
			comboBox.Items.Clear();

			// List Items
			List<ListItem> listItems = new List<ListItem>();

			// Add Standard Options
			listItems.AddRange(standardOptions.OrderBy(p => p.Text));

			// Custom Items
			if (customOptionsTextBox != null)
			{
				listItems.AddRange((customOption.GetValueFromControl(customOptionsTextBox) as ListItem[]).OrderBy(p => p.Text));
			}
			else
			{
				listItems.AddRange((customOption.GetValue(pluginProperties) as ListItem[]).OrderBy(p => p.Text));
			}

			// Add
			comboBox.Items.AddRange(listItems.ToArray());

			// Selection
			ListItem newSelectedItem = null;
			selectedValue = (selectedValue ?? string.Empty).Trim();
			if (selectedValue.Length > 0)
			{
				newSelectedItem = listItems.FirstOrDefault(p => string.Equals(p.Value, selectedValue, StringComparison.Ordinal));
			}
			if ((newSelectedItem == null) && (selectedItem != null))
			{
				newSelectedItem = listItems.FirstOrDefault(p => string.Equals(p.Value, selectedItem.Value, StringComparison.Ordinal));
			}
			if (newSelectedItem == null)
			{
				newSelectedItem = listItems.FirstOrDefault(p => string.Equals(p.Value, DefaultValue.ToString(), StringComparison.Ordinal));
			}

			// Set selection
			comboBox.SelectedItem = newSelectedItem ?? listItems.FirstOrDefault();

			// Change handler
			if (handleCustomTextChange && (customOptionsTextBox != null))
			{
				customOptionsTextBox.TextChanged += (s, e) =>
				{
					FillItems(comboBox, null, false, customOptionsTextBox, standardOptions, customOption, pluginProperties);
				};
			}
		}

		/// <summary>
		/// Creates an editor control
		/// </summary>
		/// <param name="parentControl">Parent Control</param>
		/// <param name="value">Option value</param>
		/// <param name="pluginProperties">Plugin properties</param>
		/// <returns>Control</returns>
		public override Control CreateEditorControl(Control parentControl, object value, IProperties pluginProperties)
		{
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <returns>Value</returns>
		public override object GetValueFromControl(Control control)
		{
			ListItem selectedItem = (control as ComboBox).SelectedItem as ListItem;
			return (selectedItem != null) ? selectedItem.Value : DefaultValue;
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <param name="value">Value to set</param>
		/// <param name="pluginProperties">Plugin properties</param>
		/// <returns>Value</returns>
		public override void SetValueToControl(Control control, object value, IProperties pluginProperties)
		{
			throw new InvalidOperationException();
		}

		/// <summary>
		/// Validates the content of the control for this option
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <param name="errorProvider">Error provider</param>
		/// <returns>True if valid. False otherwise</returns>
		public override bool Validate(Control control, ErrorProvider errorProvider)
		{
			ListItem selectedItem = (control as ComboBox).SelectedItem as ListItem;
			if (selectedItem == null)
			{
				FormHelper.ShowError(control, errorProvider, Resources.Error_FieldRequired);
				return false;
			}
			else
			{
				FormHelper.ShowError(control, errorProvider, null);
				return true;
			}
		}
		#endregion
	}
}
