using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class FontNameOption : CustomOptionListOption
	{
		#region Overrides
		/// <summary>
		/// Creates an editor control
		/// </summary>
		/// <param name="parentControl">Parent Control</param>
		/// <param name="value">Option value</param>
		/// <param name="pluginProperties">Plugin properties</param>
		/// <returns>Control</returns>
		public override Control CreateEditorControl(Control parentControl, object value, IProperties pluginProperties)
		{
			ComboBox comboBox = new ComboBox();
			comboBox.DisplayMember = "Text";
			comboBox.ValueMember = "Value";
			comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			FillItems(comboBox, (value ?? string.Empty).ToString().Trim(), true,
				parentControl.Controls.OfType<TextBox>().SingleOrDefault(c => c.Tag == Options.CustomFontsOption),
				Options.StandardFontListItems, Options.CustomFontsOption, pluginProperties);

			comboBox.Name = FullName;
			return comboBox;
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
			ComboBox comboBox = control as ComboBox;
			FillItems(comboBox, (value ?? string.Empty).ToString().Trim(), true,
				control.Parent.Controls.OfType<TextBox>().SingleOrDefault(c => c.Tag == Options.CustomFontsOption),
				Options.StandardFontListItems, Options.CustomFontsOption, pluginProperties);
		}
		#endregion
	}
}
