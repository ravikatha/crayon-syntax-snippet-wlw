using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class FontSizeOption : Option
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
			NumericUpDown fontSizeControl = new NumericUpDown() { Width = 50, Minimum = 0, Maximum = int.MaxValue, Increment = 1 };
			Label pixelsLabel1 = new Label() { Text = "px", AutoSize = true, Margin = new Padding(0, 4, 0, 0) };
			Label pixelsLabel2 = new Label() { Text = "px", AutoSize = true, Margin = new Padding(0, 4, 0, 0) };
			NumericUpDown lineHeightControl = new NumericUpDown() { Width = 50, Minimum = 0, Maximum = int.MaxValue, Increment = 1 };

			control.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			control.AutoSize = true;

			control.Controls.Add(fontSizeControl);
			control.Controls.Add(pixelsLabel1);
			control.Controls.Add(lineHeightControl);
			control.Controls.Add(pixelsLabel2);

			FontSize fontSize = value as FontSize;
			fontSizeControl.Value = fontSize.Size;
			lineHeightControl.Value = fontSize.LineHeight;

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
			NumericUpDown fontSizeControl = control.Controls.OfType<NumericUpDown>().FirstOrDefault();
			NumericUpDown lineHeightControl = control.Controls.OfType<NumericUpDown>().LastOrDefault();
			return new FontSize() {Size = (int) fontSizeControl.Value, LineHeight = (int) lineHeightControl.Value};
		}
		#endregion
	}
}
