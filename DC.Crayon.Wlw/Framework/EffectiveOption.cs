using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DC.Crayon.Wlw.Framework
{
	/// <summary>
	/// Option locations
	/// </summary>
	[Serializable]
	internal enum OptionLocation
	{
		Content,
		Plugin,
		Default
	}

	/// <summary>
	/// Effective option for application
	/// </summary>
	[Serializable]
	internal class EffectiveOption
	{
		#region Properties
		private string _group;
		/// <summary>
		/// Group name
		/// </summary>
		public string Group
		{
			get { return _group; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_group = (val.Length == 0) ? null : val;
			}
		}

		private string _name;
		/// <summary>
		/// Name
		/// </summary>
		public string Name
		{
			get { return _name; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_name = (val.Length == 0) ? null : val;
			}
		}

		/// <summary>
		/// Calculated full name
		/// </summary>
		public string FullName
		{
			get { return string.Join("_", (new[] { Group, Name }).Where(p => p != null).ToArray()); }
		}

		/// <summary>
		/// Value
		/// </summary>
		public object Value
		{
			get;
			set;
		}

		private bool _applicableToContent = true;
		/// <summary>
		/// Whether applicable to the content options
		/// </summary>
		public bool ApplicableToContent
		{
			get { return _applicableToContent; }
			set { _applicableToContent = value; }
		}

		/// <summary>
		/// Location from which the option is effective
		/// </summary>
		public OptionLocation Location
		{
			get;
			set;
		}
		#endregion

		#region Helper Methods
		/// <summary>
		/// Returns true if the group & name matches this option
		/// </summary>
		/// <param name="groupName">Group name</param>
		/// <param name="name">Option name</param>
		/// <returns>True if the group & name matches this option. False otherwise</returns>
		public bool IsOption(string groupName, string name)
		{
			groupName = (groupName ?? string.Empty).Trim();
			groupName = (groupName.Length == 0) ? null : groupName;
			name = (name ?? string.Empty).Trim();
			name = (name.Length == 0) ? null : name;
			return (Group == groupName) && (Name == name);
		}
		#endregion
	}
}
