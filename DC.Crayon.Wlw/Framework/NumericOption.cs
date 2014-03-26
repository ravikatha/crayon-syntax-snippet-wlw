using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class NumericOption : Option
	{
		#region Properties
		private int _minValue = int.MinValue;
		/// <summary>
		/// Minimum value
		/// </summary>
		public int MinValue
		{
			get { return _minValue; }
			set { _minValue = value; }
		}

		private int _maxValue = int.MaxValue;
		/// <summary>
		/// Maximum value
		/// </summary>
		public int MaxValue
		{
			get { return _maxValue; }
			set { _maxValue = value; }
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
			NumericUpDown upDown = new NumericUpDown();
			upDown.Increment = 1;
			upDown.Minimum = MinValue;
			upDown.Maximum = MaxValue;
			upDown.Value = (int)value;

			upDown.Name = FullName;
			return upDown;
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <returns>Value</returns>
		public override object GetValueFromControl(Control control)
		{
			return (int) ((control as NumericUpDown).Value);
		}
		#endregion
	}
}
