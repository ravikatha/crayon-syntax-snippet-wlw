using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DC.Crayon.Wlw
{
	[Serializable]
	internal class Tuple<TItem1Type, TItem2Type>
	{
		#region Construction
		public Tuple(TItem1Type item1 = default(TItem1Type), TItem2Type item2 = default(TItem2Type))
		{
			Item1 = item1;
			Item2 = item2;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Item 1
		/// </summary>
		public TItem1Type Item1
		{
			get;
			set;
		}

		/// <summary>
		/// Item 2
		/// </summary>
		public TItem2Type Item2
		{
			get;
			set;
		}
		#endregion
	}
}
