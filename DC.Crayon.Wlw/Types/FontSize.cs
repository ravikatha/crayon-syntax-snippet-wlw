using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DC.Crayon.Wlw
{
	[Serializable]
	internal class FontSize
	{
		#region Properties
		private int _size = 12;
		/// <summary>
		/// Font size
		/// </summary>
		public int Size
		{
			get { return _size; }
			set { _size = Math.Max(0, value); }
		}

		private int _lineHeight = 15;
		/// <summary>
		/// Line height
		/// </summary>
		public int LineHeight
		{
			get { return _lineHeight; }
			set { _lineHeight = Math.Max(0, value); }
		}
		#endregion
	}
}
