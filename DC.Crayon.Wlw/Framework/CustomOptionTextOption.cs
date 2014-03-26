using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class CustomOptionTextOption : MultilineTextOption
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
			TextBox textBox = new TextBox();
			ListItem[] listItems = value as ListItem[];

			textBox.Text = string.Join(Environment.NewLine,
				listItems.Where(l => (l != null) && (l.Value != null))
						.Select(l => l.Value + ";" + (l.Text ?? l.Value)).ToArray());
			textBox.Multiline = true;
			textBox.ScrollBars = ScrollBars.Both;
			textBox.Height = Height;

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
			string text = (control as TextBox).Text.Trim();
			string[] lines = text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)
				.Select(l => l.Trim())
				.Where(l => l.Length > 0)
				.ToArray();
			return lines.Select(l =>
			{
				string txt = null, val = null;
				string[] parts = l.Split(';')
					.Select(p => p.Trim()).ToArray();
				if (parts.Length > 0)
				{
					val = parts[0];
				}
				if (parts.Length > 1)
				{
					txt = parts[1];
				}
				if (string.IsNullOrEmpty(val))
				{
					return null;
				}
				txt = string.IsNullOrEmpty(txt) ? val : txt;
				return new ListItem(val, txt);
			}).Where(p => p != null).ToArray();
		}
		#endregion
	}
}
