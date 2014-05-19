using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DC.Crayon.Wlw
{
	[Serializable]
	public class ListItem
	{
		#region Construction
		public ListItem()
			: this (null, null)
		{
		}

		public ListItem(string value, string text = null)
		{
			Value = value;
			Text = text;
		}
		#endregion

		#region Properties
		private string _text;
		/// <summary>
		/// Text
		/// </summary>
		public string Text
		{
			get { return _text; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_text = (val.Length == 0) ? null : val;
			}
		}

		private string _value;
		/// <summary>
		/// Value
		/// </summary>
		public string Value
		{
			get { return _value; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_value = (val.Length == 0) ? null : val;
			}
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Strig conversion
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Text ?? Value;
		}

		/// <summary>
		/// Equality
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			var other = obj as ListItem;
			if (other == null)
			{
				return base.Equals(obj);
			}
			return ((Value != null) && (other.Value != null) && string.Equals(Value, other.Value, StringComparison.Ordinal))
				   || ((Value == null) && (other.Value == null) && (Text != null) && (other.Text != null) &&
					string.Equals(Text, other.Text, StringComparison.Ordinal));
		}

		/// <summary>
		/// Hash code generation
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return ((Text ?? string.Empty) + "$%$" + (Value ?? string.Empty)).GetHashCode();
		}
		#endregion
	}
}
