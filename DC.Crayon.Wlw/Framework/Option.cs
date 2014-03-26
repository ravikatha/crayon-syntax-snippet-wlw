using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	internal class Option : ICloneable
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
			get { return string.Join("_", (new [] {Group, Name}).Where(p => p != null).ToArray()); }
		}

		/// <summary>
		/// Default value
		/// </summary>
		public object DefaultValue
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
		/// Localized Group text
		/// </summary>
		public string LocalizedGroup
		{
			get { return Resources.ResourceManager.GetString("Group_" + Group, Resources.Culture) ?? Utils.NameToDisplayName(Group); }
		}

		/// <summary>
		/// Localized label text
		/// </summary>
		public string LocalizedLabel
		{
			get { return Resources.ResourceManager.GetString("Label_" + Name, Resources.Culture) ?? Utils.NameToDisplayName(Name); }
		}

		/// <summary>
		/// Localized help text
		/// </summary>
		public string LocalizedHelpText
		{
			get { return Resources.ResourceManager.GetString("Help_" + FullName, Resources.Culture) ?? Utils.NameToDisplayName(Name.Replace("_", string.Empty)); }
		}
		#endregion

		#region Cloning
		/// <summary>
		/// Clone this instance
		/// </summary>
		/// <returns>New instance</returns>
		public object Clone()
		{
			var newOption = Activator.CreateInstance(GetType()) as Option;
			foreach (FieldInfo fieldInfo in typeof (Option).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				fieldInfo.SetValue(newOption, fieldInfo.GetValue(this));
			}
			return newOption;
		}
		#endregion

		#region Overrides
		/// <summary>
		/// ToString override
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return FullName;
		}

		/// <summary>
		/// Equals override
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			Option other = obj as Option;
			if (other == null)
			{
				return base.Equals(obj);
			}
			return string.Equals(FullName, other.FullName, StringComparison.Ordinal);
		}

		/// <summary>
		/// Hash code override
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return FullName.GetHashCode();
		}
		#endregion

		#region Value Operations
		/// <summary>
		/// Retrieves this option's value from the properties dictionary
		/// </summary>
		/// <param name="properties">Properties dictionary</param>
		/// <returns>Value of this option</returns>
		public object GetValue(IProperties properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}

			string fullName = FullName;
			if (properties.Contains(fullName))
			{
				string val = properties.GetString(fullName, null);
				if (string.IsNullOrEmpty(val))
				{
					return null;
				}
				return Utils.BinaryDeserialize(Convert.FromBase64String(val));
			}
			return DefaultValue;
		}

		/// <summary>
		/// Saves a value for this option in the properties dictionary
		/// </summary>
		/// <param name="properties">Properties dictionary</param>
		/// <param name="value">Value to save</param>
		public void SetValue(IProperties properties, object value)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			string currentValue = properties.Contains(FullName) ? properties.GetString(FullName, null) : null;
			string newValue = Convert.ToBase64String(Utils.BinarySerialize(value));
			if ((currentValue == null)
			    || !string.Equals(currentValue, newValue, StringComparison.Ordinal))
			{
				properties.SetString(FullName, newValue);
				if (currentValue == null)
				{
					if (Created != null)
					{
						Created(this, new OptionValueEventArgs(newValue));
					}
				}
				else
				{
					if (Changed != null)
					{
						Changed(this, new OptionChangedEventArgs(currentValue, newValue));
					}
				}
			}
		}

		/// <summary>
		/// Checks if this option has a saved value in the properties dictionary
		/// </summary>
		/// <param name="properties">Properties dictionary</param>
		/// <returns>True if this option has a saved value in the properties dictionary. False otherwise</returns>
		public bool HasValue(IProperties properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			return properties.Contains(FullName);
		}

		/// <summary>
		/// Removes saved value of this option from the properties dictionary
		/// </summary>
		/// <param name="properties">Properties dictionary</param>
		public void RemoveValue(IProperties properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (properties.Contains(FullName))
			{
				string currentValue = properties.GetString(FullName, null);
				properties.Remove(FullName);
				if (Removed != null)
				{
					Removed(this, new OptionValueEventArgs(currentValue));
				}
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Fired when this option is saved for the first time into the properties dictionary
		/// </summary>
		public event OptionValueEventHandler Created;

		/// <summary>
		/// Fired when this option is changed
		/// </summary>
		public event OptionChangedEventHandler Changed;

		/// <summary>
		/// Fired when this option is removed from the properties dictionary
		/// </summary>
		public event OptionValueEventHandler Removed;
		#endregion

		#region User Interface
		/// <summary>
		/// Creates an editor control
		/// </summary>
		/// <param name="parentControl">Parent Control</param>
		/// <param name="value">Option value</param>
		/// <param name="pluginProperties">Plugin properties</param>
		/// <returns>Control</returns>
		public virtual Control CreateEditorControl(Control parentControl, object value, IProperties pluginProperties)
		{
			Control control = null;
			Type valueType = value.GetType();
			if (valueType == typeof(bool))
			{
				control = new CheckBox() { Checked = (bool)value, AutoCheck = true, Text = string.Empty };
			}
			else if (valueType.IsEnum)
			{
				ListItem[] listItems = Enum.GetNames(valueType)
					.Select(p => new ListItem(p, Resources.ResourceManager.GetString("Label_" + p) ?? Utils.NameToDisplayName(p)))
					.ToArray();
				ComboBox comboBox = new ComboBox();
				comboBox.DisplayMember = "Text";
				comboBox.ValueMember = "Value";
				comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				comboBox.Items.AddRange(listItems);
				comboBox.SelectedItem = listItems.SingleOrDefault(p => p.Value == value.ToString());
				control = comboBox;
			}
			else
			{
				TextBox textBox = new TextBox();
				textBox.Text = value.ToString();
				control = textBox;
			}

			// Return
			control.Name = FullName;
			return control;
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <returns>Value</returns>
		public virtual object GetValueFromControl(Control control)
		{
			Type valueType = DefaultValue.GetType();
			if (valueType == typeof(bool))
			{
				return (control as CheckBox).Checked;
			}
			else if (valueType.IsEnum)
			{
				ListItem selectedItem = (control as ComboBox).SelectedItem as ListItem;
				if (selectedItem != null)
				{
					return Enum.Parse(valueType, selectedItem.Value);
				}
				else
				{
					return Enum.GetValues(valueType).Cast<int>().OrderBy(p => p).FirstOrDefault();
				}
			}
			else
			{
				return (control as TextBox).Text.Trim();
			}
		}

		/// <summary>
		/// Retrieves the value from the given control
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <param name="value">Value to set</param>
		/// <param name="pluginProperties">Plugin properties</param>
		/// <returns>Value</returns>
		public virtual void SetValueToControl(Control control, object value, IProperties pluginProperties)
		{
			Type valueType = value.GetType();
			if (valueType == typeof(bool))
			{
				(control as CheckBox).Checked = (bool)value;
			}
			else if (valueType.IsEnum)
			{
				ComboBox comboBox = control as ComboBox;
				if (comboBox.Items.Count == 0)
				{
					ListItem[] listItems = Enum.GetNames(valueType)
						.Select(p => new ListItem(p, Resources.ResourceManager.GetString("Label_" + p) ?? Utils.NameToDisplayName(p)))
						.ToArray();
					comboBox.Items.AddRange(listItems);
				}
				comboBox.SelectedItem = comboBox.Items.Cast<ListItem>().SingleOrDefault(p => p.Value == value.ToString());
			}
			else
			{
				(control as TextBox).Text = value.ToString();
			}
		}

		/// <summary>
		/// Validates the content of the control for this option
		/// </summary>
		/// <param name="control">Editor control</param>
		/// <param name="errorProvider">Error provider</param>
		/// <returns>True if valid. False otherwise</returns>
		public virtual bool Validate(Control control, ErrorProvider errorProvider)
		{
			return true;
		}
		#endregion
	}
}
