using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DC.Crayon.Wlw.Framework
{
	/// <summary>
	/// Event args with option value
	/// </summary>
	[Serializable]
	public class OptionValueEventArgs : EventArgs
	{
		#region Construction
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value">Value</param>
		public OptionValueEventArgs(object value)
		{
			Value = value;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Value
		/// </summary>
		public object Value
		{
			get;
			private set;
		}
		#endregion
	}

	/// <summary>
	/// Value handler
	/// </summary>
	/// <param name="sender">Sender option</param>
	/// <param name="e">Event arguments</param>
	public delegate void OptionValueEventHandler(object sender, OptionValueEventArgs e);

	/// <summary>
	/// Changed event args
	/// </summary>
	[Serializable]
	public class OptionChangedEventArgs : EventArgs
	{
		#region Construction
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="oldValue">Old value</param>
		/// <param name="newValue">New value</param>
		public OptionChangedEventArgs(object oldValue, object newValue)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Old value
		/// </summary>
		public object OldValue
		{
			get;
			private set;
		}

		/// <summary>
		/// New value
		/// </summary>
		public object NewValue
		{
			get;
			private set;
		}
		#endregion
	}

	/// <summary>
	/// Change handler
	/// </summary>
	/// <param name="sender">Sender option</param>
	/// <param name="e">Event arguments</param>
	public delegate void OptionChangedEventHandler(object sender, OptionChangedEventArgs e);
}
