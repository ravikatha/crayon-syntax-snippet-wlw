using System.Net.Mime;
using System.Text.RegularExpressions;
using DC.Crayon.Wlw.Forms;
using DC.Crayon.Wlw.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class LineRangeOption : Option
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
			CustomTextBox textBox = new CustomTextBox();
			textBox.Text = value.ToString();
			textBox.Name = FullName;
			textBox.Pasted += (s, e) => textBox.Text = Utils.RemoveLineWhiteSpaces(textBox.Text, true, true, true);
			textBox.Leave += (s, e) => textBox.Text = Utils.RemoveLineWhiteSpaces(textBox.Text, true, true, true);
			return textBox;
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <returns>Value</returns>
		public override object GetValueFromControl(Control control)
		{
			return (control as TextBox).Text;
		}

		private static readonly Regex _lineRangeRegex = new Regex(@"^\s*\d+\s*(-\s*\d+)?(\s*,\s*\d+\s*(-\s*\d+)?)*$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
		/// <summary>
		/// Validates the content of the control for this option
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <param name="errorProvider">Error provider</param>
		/// <returns>True if valid. False otherwise</returns>
		public override bool Validate(Control control, ErrorProvider errorProvider)
		{
			string text = (control as TextBox).Text.Trim();
			if ((text.Length > 0) && !_lineRangeRegex.IsMatch(text))
			{
				FormHelper.ShowError(control, errorProvider, Resources.Error_InvalidLineNumberRange);
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
