using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Forms;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class MultilineTextOption : Option
	{
		#region Properties
		private int _height = 60;
		/// <summary>
		/// Height of the text box
		/// </summary>
		public int Height
		{
			get { return _height; }
			set { _height = Math.Max(1, value); }
		}

		private bool _isRequired;
		/// <summary>
		/// Whether text is required
		/// </summary>
		public bool IsRequired
		{
			get { return _isRequired; }
			set { _isRequired = value; }
		}
		#endregion

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
			textBox.Multiline = true;
			textBox.WordWrap = false;
			textBox.ScrollBars = ScrollBars.Both;
			textBox.Height = Height;
			textBox.Pasted += (s, e) => textBox.Text = Utils.UnindentLines(textBox.Text);
			textBox.Leave += (s, e) => textBox.Text = Utils.UnindentLines(textBox.Text);
			textBox.Name = FullName;
			return textBox;
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <returns>Value</returns>
		public override object GetValueFromControl(Control control)
		{
			return (control as TextBox).Text.Trim();
		}

		/// <summary>
		/// Validates the content of the control for this option
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <param name="errorProvider">Error provider</param>
		/// <returns>True if valid. False otherwise</returns>
		public override bool Validate(Control control, ErrorProvider errorProvider)
		{
			string text = (control as TextBox).Text.Trim();
			if (IsRequired && (text.Length == 0))
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
