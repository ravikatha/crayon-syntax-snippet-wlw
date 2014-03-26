using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DC.Crayon.Wlw
{
	[Serializable]
	internal enum WidthOrHeightType
	{
		Max,
		Min,
		Static
	}

	[Serializable]
	internal enum WidthOrHeightUnit
	{
		Pixels,
		Percent
	}

	[Serializable]
	internal class WidthOrHeight
	{
		#region Properties
		/// <summary>
		/// Unit type
		/// </summary>
		public WidthOrHeightType Type
		{
			get;
			set;
		}

		private decimal _value = 500;
		/// <summary>
		/// Value
		/// </summary>
		public decimal Value
		{
			get { return _value; }
			set { _value = Math.Max(0.0m, value); }
		}

		/// <summary>
		/// Units
		/// </summary>
		public WidthOrHeightUnit Unit
		{
			get;
			set;
		}
		#endregion
	}
}
