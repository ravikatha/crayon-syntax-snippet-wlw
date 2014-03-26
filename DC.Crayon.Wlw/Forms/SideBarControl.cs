using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using DC.Crayon.Wlw.Framework;
using DC.Crayon.Wlw.Properties;

namespace DC.Crayon.Wlw.Forms
{
	public partial class SideBarControl :  SmartContentEditor
	{
		private ISmartContentEditorSite _editorSite;
		private IProperties _options;
		private bool _controlsInitialized;

		public SideBarControl(ISmartContentEditorSite editorSite, IProperties options)
		{
			_options = options;
			_editorSite = editorSite;

			InitializeComponent();

			// Update values
			if (SelectedContent != null)
			{
				SetContentToControls(SelectedContent.Properties);
			}
			else
			{
				SetContentToControls(null);
			}

			// Inline & Url controls
			urlTextBox.Enabled = !inlineCheckBox.Checked;
			inlineCheckBox.CheckedChanged += (s, e) =>
			{
				urlTextBox.Enabled = !inlineCheckBox.Checked;
			};
		}

		private void SetContentToControls(IProperties properties)
		{
			bool initializeControls = !_controlsInitialized;
			_controlsInitialized = true;
			var contentControls = new Dictionary<string, Tuple<Control, Control>>
			{
				{ "Title", new Tuple<Control, Control>(titleFieldLabel, titleTextBox) },
				{ "Inline", new Tuple<Control, Control>(inlineCheckBox, inlineCheckBox) },
				{ "DontHighlight", new Tuple<Control, Control>(dontHighlightCheckBox, dontHighlightCheckBox) },
				{ "Language", new Tuple<Control, Control>(languageFieldLabel, languageComboBox) },
				{ "LineRange", new Tuple<Control, Control>(lineRangeFieldLabel, lineRangeTextBox) },
				{ "MarkedLines", new Tuple<Control, Control>(markedLinesFieldLabel, markedLinesTextBox) },
				{ "Content", new Tuple<Control, Control>(codeFieldLabel, codeTextBox) },
				{ "Url", new Tuple<Control, Control>(urlFieldLabel, urlTextBox) }
			};
			if (initializeControls)
			{
				titleLabel.Text = new ResourceManager(typeof(CrayonSnippetPlugin)).GetString("InsertableContentSource.SidebarText") ?? titleLabel.Text;
				updateButton.Text = Resources.Label_Update;
				resetButton.Text = Resources.Label_Reset;
				overridesButton.Text = Resources.Label_Override;
			}
			contentControls.ToList().ForEach(kv =>
			{
				Option o = Options.ContentOnlyOptions.FirstOrDefault(p => string.Equals(p.Name, kv.Key, StringComparison.Ordinal));
				if (initializeControls)
				{
					kv.Value.Item1.Tag = kv.Value.Item2.Tag = o;
					kv.Value.Item1.Text = o.LocalizedLabel;
					toolTip.SetToolTip(kv.Value.Item2, o.LocalizedHelpText);
					kv.Value.Item2.Validating += (s, e) =>
					{
						e.Cancel = !o.Validate(s as Control, errorProvider);
					};
				}
				o.SetValueToControl(kv.Value.Item2, (properties != null) ? o.GetValue(properties) : o.DefaultValue, _options);
			});
		}

		private void GetContentFromControls()
		{
			var contentControls = new Dictionary<string, Control>
			{
				{ "Title", titleTextBox },
				{ "Inline", inlineCheckBox },
				{ "DontHighlight", dontHighlightCheckBox },
				{ "Language", languageComboBox },
				{ "LineRange", lineRangeTextBox },
				{ "MarkedLines", markedLinesTextBox },
				{ "Content", codeTextBox },
				{ "Url", urlTextBox }
			};
			contentControls.ToList().ForEach(kv =>
			{
				Option o = Options.ContentOnlyOptions.FirstOrDefault(p => string.Equals(p.Name, kv.Key, StringComparison.Ordinal));
				o.SetValue(SelectedContent.Properties, o.GetValueFromControl(kv.Value));
			});

			// Fire event
			OnContentEdited();
		}

		#region Handlers
		protected override void OnSelectedContentChanged()
		{
			SetContentToControls(SelectedContent.Properties);
			base.OnSelectedContentChanged();
		}

		private void OnUpdate(object sender, EventArgs e)
		{
			if (ValidateChildren())
			{
				GetContentFromControls();
			}
		}

		private void OnReset(object sender, EventArgs e)
		{
			SetContentToControls(null);
		}

		private void OnOverrides(object sender, EventArgs e)
		{
			if (SelectedContent != null)
			{
				using (var contentForm = new ContentForm(SelectedContent.Properties, _options))
				{
					if (DialogResult.OK == contentForm.ShowDialog(this))
					{
						// Set data to controls
						SetContentToControls(SelectedContent.Properties);

						// Fire event
						OnContentEdited();
					}
				}
			}
		}
		#endregion
	}
}
