using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Framework;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Forms
{
	internal static class FormHelper
	{
		/// <summary>
		/// Initializes the options table
		/// </summary>
		/// <param name="settingsTable">Table</param>
		/// <param name="isPluginOptions">Whether the options are global options</param>
		public static void InitializeOptionsTable(TableLayoutPanel settingsTable, bool isPluginOptions)
		{
			int columnCount = isPluginOptions ? 3 : 4;
			settingsTable.SuspendLayout();
			try
			{
				// Set Columns
				settingsTable.ColumnCount = columnCount;
				settingsTable.ColumnStyles.Clear();
				settingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				settingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				settingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				if (!isPluginOptions)
				{
					settingsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				}

				// Rows
				settingsTable.RowCount = 0;
				settingsTable.Controls.Clear();
			}
			finally
			{
				settingsTable.ResumeLayout(true);
			}

			// Hook Resize event
			settingsTable.Resize += (s, e) => ResizeEditorControls(settingsTable);
		}

		/// <summary>
		/// Builds the options table
		/// </summary>
		/// <param name="settingsTable">Table</param>
		/// <param name="isPluginOptions">Whether the options are global options</param>
		/// <param name="toolTip">Tooltip control</param>
		/// <param name="errorProvider">Error provider</param>
		/// <param name="properties">Properties dictionary</param>
		/// <param name="pluginOptions">Plugin options</param>
		public static void BuildOptionsTable(TableLayoutPanel settingsTable, bool isPluginOptions, ToolTip toolTip, ErrorProvider errorProvider, IProperties properties, IProperties pluginOptions)
		{
			int columnCount = isPluginOptions ? 3 : 4;
			Font groupHeadFont = new Font(new Label().Font, FontStyle.Bold);
			List<Control> contentEditors = new List<Control>();

			List<Option> allOptions = new List<Option>(Options.AllOptions.Where(o => isPluginOptions || o.ApplicableToContent));
			if (!isPluginOptions)
			{
				allOptions.InsertRange(0, Options.ContentOnlyOptions);
			}

			settingsTable.SuspendLayout();
			try
			{
				// Rows
				foreach (IGrouping<string, Option> oGroup in allOptions.GroupBy(p => p.Group))
				{
					Option firstOption = oGroup.First();
					int rowIndex = settingsTable.RowCount;
					settingsTable.RowCount += 1;
					settingsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

					// Add Group Heading
					var groupHeading = new Label() { Text = firstOption.LocalizedGroup };
					groupHeading.BorderStyle = BorderStyle.FixedSingle;
					groupHeading.Font = groupHeadFont;
					groupHeading.AutoEllipsis = true;
					groupHeading.AutoSize = false;
					groupHeading.Dock = DockStyle.Fill;
					groupHeading.Padding = new Padding(0, 4, 0, 0);
					settingsTable.Controls.Add(groupHeading, 0, rowIndex);
					settingsTable.SetColumnSpan(groupHeading, columnCount);

					// Properties
					foreach (Option o in oGroup)
					{
						int col = 0;
						rowIndex = settingsTable.RowCount;
						settingsTable.RowCount += 1;
						settingsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

						// Add Setting Label
						Label label = new Label() { Text = o.LocalizedLabel };
						label.AutoEllipsis = true;
						label.AutoSize = true;
						label.Padding = new Padding(0, 7, 0, 0);
						label.Tag = o;
						settingsTable.Controls.Add(label, col++, rowIndex);

						// Checkboxes
						CheckBox overrideCheckbox = null;
						CheckBox useDefaultCheckbox = null;

						// Value
						object propVal = (properties != null) ? o.GetValue(properties) : o.DefaultValue;

						// Is Content Option
						bool contentOption = Options.ContentOnlyOptions.Contains(o);
						if (!contentOption)
						{
							// Override Checkbox
							overrideCheckbox = new CheckBox() { Checked = (properties != null) && o.HasValue(properties), Text = Resources.Label_Override };
							overrideCheckbox.Tag = o;
							toolTip.SetToolTip(overrideCheckbox, Resources.Help_Override);
							settingsTable.Controls.Add(overrideCheckbox, col++, rowIndex);

							// Override Checkbox
							if (!isPluginOptions)
							{
								useDefaultCheckbox = new CheckBox() { Checked = Options.IsDefault(propVal), Text = Resources.Label_UseDefault };
								useDefaultCheckbox.Tag = o;
								toolTip.SetToolTip(useDefaultCheckbox, Resources.Help_UseDefault);
								settingsTable.Controls.Add(useDefaultCheckbox, col++, rowIndex);
							}
						}

						// Add Control
						Control editorControl = o.CreateEditorControl(settingsTable, propVal, pluginOptions);
						toolTip.SetToolTip(editorControl, o.LocalizedHelpText);
						editorControl.Tag = o;
						editorControl.Validating += (s, e) =>
						{
							e.Cancel = !o.Validate(s as Control, errorProvider);
						};
						settingsTable.Controls.Add(editorControl, col++, rowIndex);
						contentEditors.Add(editorControl);
						if (contentOption)
						{
							settingsTable.SetColumnSpan(editorControl, 3);
						}

						if (!contentOption)
						{
							UpdateOptionCheckBoxesAndEditorControls(overrideCheckbox, useDefaultCheckbox, editorControl);
							overrideCheckbox.CheckedChanged += (s, e) =>
							{
								UpdateOptionCheckBoxesAndEditorControls(overrideCheckbox, useDefaultCheckbox, editorControl);
								ResizeEditorControls(settingsTable);
							};
							if (useDefaultCheckbox != null)
							{
								useDefaultCheckbox.CheckedChanged += (s, e) =>
								{
									UpdateOptionCheckBoxesAndEditorControls(overrideCheckbox, useDefaultCheckbox, editorControl);
									ResizeEditorControls(settingsTable);
								};
							}
						}
					}
				}
			}
			finally
			{
				settingsTable.ResumeLayout(true);
				ResizeEditorControls(settingsTable);
			}
		}

		/// <summary>
		/// Resizes editor controls to fit the last column
		/// </summary>
		/// <param name="tablePanel"></param>
		private static void ResizeEditorControls(TableLayoutPanel tablePanel)
		{
			tablePanel.SuspendLayout();
			try
			{
				// Make editors small
				for (int row = 0; row < tablePanel.RowCount; ++row)
				{
					Control editorControl = tablePanel.GetControlFromPosition(tablePanel.ColumnCount - 1, row);
					Option o = (editorControl != null) ? editorControl.Tag as Option : null;
					if ((o == null) || (editorControl is Label))
					{
						continue;
					}

					// Change
					editorControl.Anchor = AnchorStyles.None;
					editorControl.Width = 5;
				}

				// Layout once
				tablePanel.ResumeLayout(true);
				tablePanel.SuspendLayout();

				// Size now
				int[] colWidths = tablePanel.GetColumnWidths();
				for (int row = 0; row < tablePanel.RowCount; ++row)
				{
					Control editorControl = tablePanel.GetControlFromPosition(tablePanel.ColumnCount - 1, row);
					Option o = (editorControl != null) ? editorControl.Tag as Option : null;
					if ((o == null) || (editorControl is Label))
					{
						continue;
					}

					// Change
					int colWidth = 0;
					for (int col = 0; col < tablePanel.GetColumnSpan(editorControl); col++)
					{
						colWidth += colWidths[colWidths.Length - 1 - col];
					}
					editorControl.Width = colWidth - 10;
					editorControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
				}
			}
			finally
			{
				tablePanel.ResumeLayout(true);
			}
		}

		private static void UpdateOptionCheckBoxesAndEditorControls(CheckBox overrideCheckbox, CheckBox useDefaultCheckbox, Control editorControl)
		{
			if (useDefaultCheckbox != null)
			{
				useDefaultCheckbox.Visible = overrideCheckbox.Checked;
				editorControl.Visible = overrideCheckbox.Checked && !useDefaultCheckbox.Checked;
			}
			else
			{
				editorControl.Visible = overrideCheckbox.Checked;
			}
		}

		/// <summary>
		/// Saves the options from the table to the underlying properties dictionary
		/// </summary>
		/// <param name="tableLayout">Table</param>
		/// <param name="isPluginOptions">Whether the options are global options</param>
		/// <param name="pluginOptions">Plugin options</param>
		public static void SaveOptions(TableLayoutPanel tableLayout, bool isPluginOptions, IProperties pluginOptions)
		{
			for (int row = 0; row < tableLayout.RowCount; ++row)
			{
				var o = tableLayout.GetControlFromPosition(0, row).Tag as Option;
				if (o == null)
				{
					continue;
				}

				int col = 1;
				bool contentOption = Options.ContentOnlyOptions.Contains(o);
				CheckBox overrideCheckbox = !contentOption ? tableLayout.GetControlFromPosition(col++, row) as CheckBox : null;
				CheckBox useDefaultCheckbox = (!contentOption && !isPluginOptions) ? tableLayout.GetControlFromPosition(col++, row) as CheckBox : null;
				Control editorControl = tableLayout.GetControlFromPosition(col, row);

				if (contentOption)
				{
					o.SetValue(pluginOptions, o.GetValueFromControl(editorControl));
				}
				else if (isPluginOptions)
				{
					if (!overrideCheckbox.Checked)
					{
						o.RemoveValue(pluginOptions);
					}
					else
					{
						o.SetValue(pluginOptions, o.GetValueFromControl(editorControl));
					}
				}
				else
				{
					if (!overrideCheckbox.Checked)
					{
						o.RemoveValue(pluginOptions);
					}
					else
					{
						if (useDefaultCheckbox.Checked)
						{
							o.SetValue(pluginOptions, Options.Default);
						}
						else
						{
							o.SetValue(pluginOptions, o.GetValueFromControl(editorControl));
						}
					}
				}
			}
		}

		/// <summary>
		/// Shows error message for the given control
		/// </summary>
		/// <param name="control">Control</param>
		/// <param name="errorProvider">Error provider</param>
		/// <param name="error">Error message</param>
		public static void ShowError(Control control, ErrorProvider errorProvider, string error)
		{
			// Current error
			error = (error ?? string.Empty).Trim();
			error = (error.Length == 0) ? null : error;

			// Previous error
			string existingError = errorProvider.GetError(control);
			existingError = (existingError ?? string.Empty).Trim();
			existingError = (existingError.Length == 0) ? null : existingError;

			// Apply
			if (error != null)
			{
				if (existingError == null)
				{
					control.Width -= 32;
				}
				errorProvider.SetError(control, error);
			}
			else
			{
				errorProvider.SetError(control, null);
				if (existingError != null)
				{
					control.Width += 32;
				}
			}
		}
	}
}
