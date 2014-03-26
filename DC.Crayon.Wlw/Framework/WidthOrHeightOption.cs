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
	internal class WidthOrHeightOption : Option
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
			FlowLayoutPanel control = new FlowLayoutPanel();
			ComboBox sizeType = new ComboBox() { DisplayMember = "Text", ValueMember = "Value", DropDownStyle = ComboBoxStyle.DropDownList };
			NumericUpDown valueControl = new NumericUpDown() { Width = 50, Minimum = decimal.MinValue, Maximum = decimal.MaxValue, Increment = 1m };
			ComboBox unitType = new ComboBox() { DisplayMember = "Text", ValueMember = "Value", DropDownStyle = ComboBoxStyle.DropDownList };

			control.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			control.AutoSize = true;

			control.Controls.Add(sizeType);
			control.Controls.Add(valueControl);
			control.Controls.Add(unitType);

			sizeType.DisplayMember = "Text";
			sizeType.ValueMember = "Value";
			sizeType.DropDownStyle = ComboBoxStyle.DropDownList; ;
			sizeType.Items.AddRange(Enum.GetNames(typeof(WidthOrHeightType))
					.Select(p => new ListItem(p, Resources.ResourceManager.GetString("Label_" + p) ?? Utils.NameToDisplayName(p)))
					.ToArray());

			unitType.DisplayMember = "Text";
			unitType.ValueMember = "Value";
			unitType.DropDownStyle = ComboBoxStyle.DropDownList; ;
			unitType.Items.AddRange(Enum.GetNames(typeof(WidthOrHeightUnit))
					.Select(p => new ListItem(p, Resources.ResourceManager.GetString("Label_" + p) ?? Utils.NameToDisplayName(p)))
					.ToArray());

			WidthOrHeight widthOrHeight = value as WidthOrHeight;
			sizeType.SelectedItem = sizeType.Items.Cast<ListItem>().FirstOrDefault(p => string.Equals(p.Value, widthOrHeight.Type.ToString(), StringComparison.Ordinal));
			unitType.SelectedItem = unitType.Items.Cast<ListItem>().FirstOrDefault(p => string.Equals(p.Value, widthOrHeight.Unit.ToString(), StringComparison.Ordinal));
			valueControl.Value = widthOrHeight.Value;

			// Return
			control.Name = FullName;
			return control;
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <returns>Value</returns>
		public override object GetValueFromControl(Control control)
		{
			ComboBox sizeType = control.Controls.OfType<ComboBox>().FirstOrDefault();
			NumericUpDown valueControl = control.Controls.OfType<NumericUpDown>().FirstOrDefault();
			ComboBox unitType = control.Controls.OfType<ComboBox>().LastOrDefault();
			return new WidthOrHeight()
			{
				Type = (WidthOrHeightType) Enum.Parse(typeof(WidthOrHeightType), (sizeType.SelectedItem as ListItem).Value),
				Value = valueControl.Value,
				Unit = (WidthOrHeightUnit)Enum.Parse(typeof(WidthOrHeightUnit), (unitType.SelectedItem as ListItem).Value)
			};
		}
		#endregion
	}
}
