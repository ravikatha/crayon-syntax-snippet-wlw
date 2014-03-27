using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;
using WindowsLive.Writer.Api;
using System.Windows.Forms;
using DC.Crayon.Wlw.Forms;
using DC.Crayon.Wlw.Framework;

namespace DC.Crayon.Wlw
{
	[WriterPlugin("DF523902-42FF-4D05-9A03-77A1052AB9D9", "Crayon Code Snippet from DotCastle",
				Description = "Crayon Code Snippet Plug-in from DotCastle for Windows Live Writer",
				ImagePath = "Resources.Icon.png",
				HasEditableOptions = true, PublisherUrl = "https://github.com/dotcastle/crayon-syntax-snippet-wlw")]
	[InsertableContentSource("Insert Crayon Code Snippet from DotCastle", SidebarText = "Crayon Snippet from DotCastle")]
	public class CrayonSnippetPlugin : SmartContentSource
	{
		private static readonly Regex _whiteSpaceRegex = new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.Singleline);
		private const string _elementIdProperty = "Editor_ElementId";

		#region Initialization
		/// <summary>
		/// Initialization override for update management
		/// </summary>
		/// <param name="pluginOptions"></param>
		public override void Initialize(IProperties pluginOptions)
		{
			// Base
			base.Initialize(pluginOptions);

			// Initiate update management
			bool checkAtStartup = pluginOptions.GetBoolean(Updater.CheckUpdatesOnStartupOptionName, true);
			if (checkAtStartup)
			{
				Updater.Check(pluginOptions);
			}
		}
		#endregion

		#region Support Methods
		public override void EditOptions(IWin32Window dialogOwner)
		{
			using (var optionsForm = new OptionsForm(Options))
			{
				optionsForm.ShowDialog(dialogOwner);
			}
		}

		public override DialogResult CreateContent(IWin32Window dialogOwner, ISmartContent newContent)
		{
			using (var contentForm = new ContentForm(newContent.Properties, Options))
			{
				return contentForm.ShowDialog(dialogOwner);
			}
		}

		public override SmartContentEditor CreateEditor(ISmartContentEditorSite editorSite)
		{
			return new SideBarControl(editorSite, Options);
		}
		#endregion

		#region Resizing
		/// <summary>
		/// Resizing capabilities
		/// </summary>
		public override ResizeCapabilities ResizeCapabilities
		{
			get { return ResizeCapabilities.Resizable | ResizeCapabilities.LiveResize; }
		}

		/// <summary>
		/// Resize started
		/// </summary>
		/// <param name="content"></param>
		/// <param name="options"></param>
		public override void OnResizeStart(ISmartContent content, ResizeOptions options)
		{
			// Check if we have editor mode element ID
			if (content.Properties.Contains(_elementIdProperty))
			{
				string elementId = content.Properties.GetString(_elementIdProperty, null);
				if (elementId != null)
				{
					options.ResizeableElementId = elementId;
				}
			}
		}

		/// <summary>
		/// Resizing completion event
		/// </summary>
		/// <param name="content"></param>
		/// <param name="newSize"></param>
		public override void OnResizeComplete(ISmartContent content, Size newSize)
		{
			// Base
			base.OnResizeComplete(content, newSize);

			// Update
			DC.Crayon.Wlw.Framework.Options.UpdateContentSize(content.Properties, newSize);
		}
		#endregion

		#region Html Generation
		/// <summary>
		/// Generates HTML for the editor mode
		/// </summary>
		/// <param name="content">Smart content object</param>
		/// <param name="publishingContext">Publishing context</param>
		/// <returns>Html</returns>
		public override string GenerateEditorHtml(ISmartContent content, IPublishingContext publishingContext)
		{
			// Get non-default options
			EffectiveOption[] options = DC.Crayon.Wlw.Framework.
				Options.GetEffectiveOptions(content.Properties, Options)
					.Where(p => p.Location != OptionLocation.Default)
					.ToArray();

			// Html Element
			HtmlElement preElement = new HtmlElement() { Tag = "pre" };
			OrderedDictionary classAttrParts = new OrderedDictionary(StringComparer.Ordinal);
			OrderedDictionary styleAttr = new OrderedDictionary(StringComparer.Ordinal);

			// ID
			string elementId = Guid.NewGuid().ToString("N");
			preElement.Attributes["id"] = elementId;
			content.Properties[_elementIdProperty] = elementId;

			// Decode
			classAttrParts["decode"] = "true";

			// Setting Groups
			ApplyContentSettings(true, preElement, classAttrParts, styleAttr, options);
			ApplyMetricsSettings(true, preElement, classAttrParts, styleAttr, options);
			ApplyToolbarSettings(true, preElement, classAttrParts, styleAttr, options);
			ApplyLinesSettings(true, preElement, classAttrParts, styleAttr, options);
			ApplyCodeSettings(true, preElement, classAttrParts, styleAttr, options);

			// Set Class
			if (classAttrParts.Count > 0)
			{
				preElement.Attributes["class"] = string.Join(" ",
					classAttrParts.Cast<DictionaryEntry>()
					.Select(kv => string.Format(CultureInfo.InvariantCulture, "{0}:{1}", kv.Key, HtmlElement.EscapeAttributeValue(kv.Value.ToString())))
					.ToArray());
			}

			// Set Style
			if (styleAttr.Count > 0)
			{
				preElement.Attributes["style"] = string.Join("; ",
					styleAttr.Cast<DictionaryEntry>()
					.Select(kv => string.Format(CultureInfo.InvariantCulture, "{0}:{1}", kv.Key, HtmlElement.EscapeAttributeValue(kv.Value.ToString())))
					.ToArray());
			}

			// Return
			return preElement.ToString();
		}

		/// <summary>
		/// Generates HTML for the publishing mode
		/// </summary>
		/// <param name="content">Smart content object</param>
		/// <param name="publishingContext">Publishing context</param>
		/// <returns>Html</returns>
		public override string GeneratePublishHtml(ISmartContent content, IPublishingContext publishingContext)
		{
			// Get non-default options
			EffectiveOption[] options = DC.Crayon.Wlw.Framework.
				Options.GetEffectiveOptions(content.Properties, Options)
					.Where(p => p.Location != OptionLocation.Default)
					.ToArray();

			// Html Element
			HtmlElement preElement = new HtmlElement() { Tag = "pre" };
			OrderedDictionary classAttrParts = new OrderedDictionary(StringComparer.Ordinal);
			OrderedDictionary styleAttr = new OrderedDictionary(StringComparer.Ordinal);

			// Decode
			classAttrParts["decode"] = "true";

			// Setting Groups
			ApplyContentSettings(false, preElement, classAttrParts, styleAttr, options);
			ApplyMetricsSettings(false, preElement, classAttrParts, styleAttr, options);
			ApplyToolbarSettings(true, preElement, classAttrParts, styleAttr, options);
			ApplyLinesSettings(true, preElement, classAttrParts, styleAttr, options);
			ApplyCodeSettings(true, preElement, classAttrParts, styleAttr, options);

			// Set Class
			if (classAttrParts.Count > 0)
			{
				preElement.Attributes["class"] = string.Join(" ",
					classAttrParts.Cast<DictionaryEntry>()
					.Select(kv => string.Format(CultureInfo.InvariantCulture, "{0}:{1}", kv.Key, HtmlElement.EscapeAttributeValue(kv.Value.ToString())))
					.ToArray());
			}

			// Return
			return preElement.ToString();
		}
		#endregion

		#region Groups Html Generation
		/// <summary>
		/// Content Settings
		/// </summary>
		/// <param name="editorMode">Editor mode</param>
		/// <param name="preElement">Html Element</param>
		/// <param name="classAttrParts">Class attribute parts dictionary</param>
		/// <param name="styleAttr">Style dictionary</param>
		/// <param name="options">Effective options dictionary</param>
		private void ApplyContentSettings(bool editorMode, HtmlElement preElement, OrderedDictionary classAttrParts, OrderedDictionary styleAttr, EffectiveOption[] options)
		{
			EffectiveOption option = null;
			string groupName = "Content";

			// Title
			option = options.SingleOrDefault(o => o.IsOption(groupName, "Title"));
			if (option != null)
			{
				string valText = option.Value.ToString().Trim();
				if (valText.Length > 0)
				{
					preElement.Attributes["title"] = valText;
				}
			}

			// Inline & Url
			option = options.SingleOrDefault(o => o.IsOption(groupName, "Inline"));
			if ((option == null) || !(bool)option.Value)
			{
				option = options.SingleOrDefault(o => o.IsOption(groupName, "Url"));
				if (option != null)
				{
					string valText = option.Value.ToString().Trim();
					if (valText.Length > 0)
					{
						preElement.Attributes["data-url"] = valText;
					}
				}
			}

			// DontHighlight
			option = options.SingleOrDefault(o => o.IsOption(groupName, "DontHighlight"));
			if (option != null)
			{
				classAttrParts["highlight"] = (!((bool) option.Value)).ToString().ToLowerInvariant();
			}

			// Language
			option = options.SingleOrDefault(o => o.IsOption(groupName, "Language"));
			if (option != null)
			{
				string valText = option.Value.ToString().Trim();
				if (valText.Length > 0)
				{
					classAttrParts["lang"] = valText;
				}
			}

			// Line Range
			option = options.SingleOrDefault(o => o.IsOption(groupName, "LineRange"));
			if (option != null)
			{
				string valText = _whiteSpaceRegex.Replace(option.Value.ToString().Trim(), string.Empty);
				if (valText.Length > 0)
				{
					classAttrParts["range"] = valText;
				}
			}

			// Marked Lines
			option = options.SingleOrDefault(o => o.IsOption(groupName, "MarkedLines"));
			if (option != null)
			{
				string valText = _whiteSpaceRegex.Replace(option.Value.ToString().Trim(), string.Empty);
				if (valText.Length > 0)
				{
					classAttrParts["mark"] = valText;
				}
			}

			// Content
			option = options.SingleOrDefault(o => o.IsOption(groupName, "Content"));
			if (option != null)
			{
				preElement.Text = option.Value.ToString().Trim();
			}

			// Theme
			option = options.SingleOrDefault(o => o.IsOption("Theme", "Name"));
			if (option != null)
			{
				string valText = option.Value.ToString().Trim();
				if (valText.Length > 0)
				{
					classAttrParts["theme"] = valText;
				}
			}

			// Font Name
			option = options.SingleOrDefault(o => o.IsOption("Font", "Name"));
			if (option != null)
			{
				string valText = option.Value.ToString().Trim();
				if (valText.Length > 0)
				{
					classAttrParts["font"] = valText;
					styleAttr["font-family"] = valText;
				}
			}

			// Font Size
			option = options.SingleOrDefault(o => o.IsOption("Font", "FontSize"));
			if (option != null)
			{
				FontSize fontSize = option.Value as FontSize;
				if (fontSize != null)
				{
					classAttrParts["font-size"] = fontSize.Size.ToString(CultureInfo.InvariantCulture);
					styleAttr["font-size"] = fontSize.Size.ToString(CultureInfo.InvariantCulture) + "px";

					classAttrParts["line-height"] = fontSize.LineHeight.ToString(CultureInfo.InvariantCulture);
					styleAttr["line-height"] = fontSize.LineHeight.ToString(CultureInfo.InvariantCulture) + "px";
				}
			}
		}

		/// <summary>
		/// Metrics Settings
		/// </summary>
		/// <param name="editorMode">Editor mode</param>
		/// <param name="preElement">Html Element</param>
		/// <param name="classAttrParts">Class attribute parts dictionary</param>
		/// <param name="styleAttr">Style dictionary</param>
		/// <param name="options">Effective options dictionary</param>
		private void ApplyMetricsSettings(bool editorMode, HtmlElement preElement, OrderedDictionary classAttrParts, OrderedDictionary styleAttr, EffectiveOption[] options)
		{
			EffectiveOption option = null;
			string groupName = "Metrics";

			// Defaults
			styleAttr["box-sizing"] = "border-box";
			styleAttr["-moz-box-sizing"] = "border-box";
			styleAttr["margin"] = "0";
			styleAttr["padding"] = "0";
			styleAttr["border"] = "0 none transparent";

			// Width
			option = options.SingleOrDefault(o => o.IsOption(groupName, "Width"));
			if (option != null)
			{
				WidthOrHeight val = option.Value as WidthOrHeight;
				if (val != null)
				{
					classAttrParts["width-set"] = "true";
					classAttrParts["width-mode"] = ((int)val.Type).ToString(CultureInfo.InvariantCulture);
					classAttrParts["width-unit"] = ((int)val.Unit).ToString(CultureInfo.InvariantCulture);

					if (val.Type == WidthOrHeightType.Min)
					{
						styleAttr["min-width"] = val.Value + ((val.Unit == WidthOrHeightUnit.Percent) ? "%" : "px");
					}
					else if (val.Type == WidthOrHeightType.Max)
					{
						styleAttr["max-width"] = val.Value + ((val.Unit == WidthOrHeightUnit.Percent) ? "%" : "px");
					}
					else
					{
						styleAttr["width"] = val.Value + ((val.Unit == WidthOrHeightUnit.Percent) ? "%" : "px");
					}
				}
			}

			// Height
			option = options.SingleOrDefault(o => o.IsOption(groupName, "Height"));
			if (option != null)
			{
				WidthOrHeight val = option.Value as WidthOrHeight;
				if (val != null)
				{
					classAttrParts["height-set"] = "true";
					classAttrParts["height-mode"] = ((int)val.Type).ToString(CultureInfo.InvariantCulture);
					classAttrParts["height-unit"] = ((int)val.Unit).ToString(CultureInfo.InvariantCulture);

					if (val.Type == WidthOrHeightType.Min)
					{
						styleAttr["min-height"] = val.Value + ((val.Unit == WidthOrHeightUnit.Percent) ? "%" : "px");
					}
					else if (val.Type == WidthOrHeightType.Max)
					{
						styleAttr["max-height"] = val.Value + ((val.Unit == WidthOrHeightUnit.Percent) ? "%" : "px");
					}
					else
					{
						styleAttr["height"] = val.Value + ((val.Unit == WidthOrHeightUnit.Percent) ? "%" : "px");
					}
				}
			}

			// Margins, Paddings & Borders
			var marginTypes = new[] {"top", "right", "bottom", "left"};
			var marginOptions = marginTypes
				.Select(m => new { Type = m, Option = options.SingleOrDefault(o => o.IsOption(groupName, m.Substring(0, 1).ToUpperInvariant() + m.Substring(1) + "Margin")) })
				.Where(o => o.Option != null)
				.ToArray();
			marginOptions.ToList().ForEach(o =>
			{
				classAttrParts[o.Type + "-margin"] = ((int) o.Option.Value).ToString(CultureInfo.InvariantCulture);
				styleAttr["margin-" + o.Type] = ((int)o.Option.Value).ToString(CultureInfo.InvariantCulture) + "px";
			});

			// Horizontal Alignment
			option = options.SingleOrDefault(o => o.IsOption(groupName, "HorizontalAlignment"));
			if (option != null)
			{
				HorizontalAlignment val = (HorizontalAlignment) option.Value;
				if (val != HorizontalAlignment.None)
				{
					classAttrParts["h-align"] = ((int)val).ToString(CultureInfo.InvariantCulture);
					if (val == HorizontalAlignment.Left)
					{
						styleAttr["text-align"] = "left";
					}
					else if (val == HorizontalAlignment.Right)
					{
						styleAttr["text-align"] = "right";
					}
					else if (val == HorizontalAlignment.Center)
					{
						styleAttr["text-align"] = "center";
					}
				}
			}

			// AllowFloatingElementsToSurroundCrayon
			option = options.SingleOrDefault(o => o.IsOption(groupName, "HorizontalAlignment"));
			if (option != null)
			{
				bool val = (bool) option.Value;
				classAttrParts["float-enable"] = val.ToString().ToLowerInvariant();
			}

			// InlineMargin
			option = options.SingleOrDefault(o => o.IsOption(groupName, "InlineMargin"));
			if (option != null)
			{
				int val = (int)option.Value;
				classAttrParts["inline-margin"] = val.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
			}
		}

		/// <summary>
		/// ToolBar Settings
		/// </summary>
		/// <param name="editorMode">Editor mode</param>
		/// <param name="preElement">Html Element</param>
		/// <param name="classAttrParts">Class attribute parts dictionary</param>
		/// <param name="styleAttr">Style dictionary</param>
		/// <param name="options">Effective options dictionary</param>
		private void ApplyToolbarSettings(bool editorMode, HtmlElement preElement, OrderedDictionary classAttrParts, OrderedDictionary styleAttr, EffectiveOption[] options)
		{
			EffectiveOption option = null;
			string groupName = "Toolbar";

			// Display
			option = options.SingleOrDefault(o => o.IsOption(groupName, "Display"));
			if (option != null)
			{
				ToolbarDisplay val = (ToolbarDisplay) option.Value;
				classAttrParts["toolbar"] = ((int) val).ToString(CultureInfo.InvariantCulture);
			}

			// OverlayOnCode
			option = options.SingleOrDefault(o => o.IsOption(groupName, "OverlayOnCode"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["toolbar-overlay"] = val.ToString().ToLowerInvariant();
			}

			// ToggleOnSingleClickWhenOverlayed
			option = options.SingleOrDefault(o => o.IsOption(groupName, "ToggleOnSingleClickWhenOverlayed"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["toolbar-hide"] = val.ToString().ToLowerInvariant();
			}

			// DelayHideOnMouseOut
			option = options.SingleOrDefault(o => o.IsOption(groupName, "DelayHideOnMouseOut"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["toolbar-delay"] = val.ToString().ToLowerInvariant();
			}

			// ShowTitle
			option = options.SingleOrDefault(o => o.IsOption(groupName, "ShowTitle"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["show-title"] = val.ToString().ToLowerInvariant();
			}

			// LanguageDisplay
			option = options.SingleOrDefault(o => o.IsOption(groupName, "LanguageDisplay"));
			if (option != null)
			{
				LanguageDisplay val = (LanguageDisplay)option.Value;
				classAttrParts["show-lang"] = ((int)val).ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>
		/// Lines Settings
		/// </summary>
		/// <param name="editorMode">Editor mode</param>
		/// <param name="preElement">Html Element</param>
		/// <param name="classAttrParts">Class attribute parts dictionary</param>
		/// <param name="styleAttr">Style dictionary</param>
		/// <param name="options">Effective options dictionary</param>
		private void ApplyLinesSettings(bool editorMode, HtmlElement preElement, OrderedDictionary classAttrParts, OrderedDictionary styleAttr, EffectiveOption[] options)
		{
			EffectiveOption option = null;
			string groupName = "Lines";

			// StripedLines
			option = options.SingleOrDefault(o => o.IsOption(groupName, "StripedLines"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["striped"] = val.ToString().ToLowerInvariant();
			}

			// EnableLineMarking
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnableLineMarking"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["marking"] = val.ToString().ToLowerInvariant();
			}

			// EnableLineRanges
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnableLineRanges"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["ranges"] = val.ToString().ToLowerInvariant();
			}

			// DisplayNumbersByDefault
			option = options.SingleOrDefault(o => o.IsOption(groupName, "DisplayNumbersByDefault"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["nums"] = val.ToString().ToLowerInvariant();
			}

			// EnableLineNumberToggling
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnableLineNumberToggling"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["nums-toggle"] = val.ToString().ToLowerInvariant();
			}

			// WrapLinesByDefault
			option = options.SingleOrDefault(o => o.IsOption(groupName, "WrapLinesByDefault"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["wrap"] = val.ToString().ToLowerInvariant();
			}

			// EnableLineWrapToggling
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnableLineWrapToggling"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["wrap-toggle"] = val.ToString().ToLowerInvariant();
			}

			// StartingLineNumber
			option = options.SingleOrDefault(o => o.IsOption(groupName, "StartingLineNumber"));
			if (option != null)
			{
				int val = (int)option.Value;
				classAttrParts["start-line"] = val.ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>
		/// Code Settings
		/// </summary>
		/// <param name="editorMode">Editor mode</param>
		/// <param name="preElement">Html Element</param>
		/// <param name="classAttrParts">Class attribute parts dictionary</param>
		/// <param name="styleAttr">Style dictionary</param>
		/// <param name="options">Effective options dictionary</param>
		private void ApplyCodeSettings(bool editorMode, HtmlElement preElement, OrderedDictionary classAttrParts, OrderedDictionary styleAttr, EffectiveOption[] options)
		{
			EffectiveOption option = null;
			string groupName = "Code";

			// EnablePlainCodeView
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnablePlainCodeView"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["plain"] = val.ToString().ToLowerInvariant();
			}

			// PlainCodeViewDisplayTrigger
			option = options.SingleOrDefault(o => o.IsOption(groupName, "PlainCodeViewDisplayTrigger"));
			if (option != null)
			{
				PlainCodeViewDisplayMouseTrigger val = (PlainCodeViewDisplayMouseTrigger)option.Value;
				classAttrParts["show-plain"] = ((int)val).ToString(CultureInfo.InvariantCulture);
			}

			// EnablePlainCodeToggling
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnablePlainCodeToggling"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["plain-toggle"] = val.ToString().ToLowerInvariant();
			}

			// ShowPlainCodeByDefault
			option = options.SingleOrDefault(o => o.IsOption(groupName, "ShowPlainCodeByDefault"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["show-plain-default"] = val.ToString().ToLowerInvariant();
			}

			// EnableCopyPaste
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnableCopyPaste"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["copy"] = val.ToString().ToLowerInvariant();
			}

			// EnableOpenInNewWindow
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnableOpenInNewWindow"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["popup"] = val.ToString().ToLowerInvariant();
			}

			// AlwaysDisplayScrollbars
			option = options.SingleOrDefault(o => o.IsOption(groupName, "AlwaysDisplayScrollbars"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["scroll"] = val.ToString().ToLowerInvariant();
			}

			// MinimizeCode
			option = options.SingleOrDefault(o => o.IsOption(groupName, "MinimizeCode"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["minimize"] = val.ToString().ToLowerInvariant();
			}

			// ExpandCodeBeyondPageBorderOnMouseHover
			option = options.SingleOrDefault(o => o.IsOption(groupName, "ExpandCodeBeyondPageBorderOnMouseHover"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["expand"] = val.ToString().ToLowerInvariant();
			}

			// EnableCodeExpandingToggling
			option = options.SingleOrDefault(o => o.IsOption(groupName, "EnableCodeExpandingToggling"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["expand-toggle"] = val.ToString().ToLowerInvariant();
			}

			// DecodeHtmlEntitiesInAttributes
			option = options.SingleOrDefault(o => o.IsOption(groupName, "DecodeHtmlEntitiesInAttributes"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["decode-attributes"] = val.ToString().ToLowerInvariant();
			}

			// RemoveWhitespacesSurroundingShortcodeContent
			option = options.SingleOrDefault(o => o.IsOption(groupName, "RemoveWhitespacesSurroundingShortcodeContent"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["trim-whitespace"] = val.ToString().ToLowerInvariant();
			}

			// RemoveCodeTagsSurroundingShortcodeContent
			option = options.SingleOrDefault(o => o.IsOption(groupName, "RemoveCodeTagsSurroundingShortcodeContent"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["trim-code-tag"] = val.ToString().ToLowerInvariant();
			}

			// AllowMixedLanguageHighlightingWithDelimitersAndTags
			option = options.SingleOrDefault(o => o.IsOption(groupName, "AllowMixedLanguageHighlightingWithDelimitersAndTags"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["mixed"] = val.ToString().ToLowerInvariant();
			}

			// ShowMixedLanguageIcon
			option = options.SingleOrDefault(o => o.IsOption(groupName, "ShowMixedLanguageIcon"));
			if (option != null)
			{
				bool val = (bool)option.Value;
				classAttrParts["show_mixed"] = val.ToString().ToLowerInvariant();
			}

			// TabSizeInSpaces
			option = options.SingleOrDefault(o => o.IsOption(groupName, "TabSizeInSpaces"));
			if (option != null)
			{
				int val = (int)option.Value;
				classAttrParts["tab-size"] = val.ToString(CultureInfo.InvariantCulture);
			}

			// BlankLinesBeforeCode
			option = options.SingleOrDefault(o => o.IsOption(groupName, "BlankLinesBeforeCode"));
			if (option != null)
			{
				int val = (int)option.Value;
				classAttrParts["whitespace-before"] = val.ToString(CultureInfo.InvariantCulture);
			}

			// BlankLinesAfterCode
			option = options.SingleOrDefault(o => o.IsOption(groupName, "BlankLinesAfterCode"));
			if (option != null)
			{
				int val = (int)option.Value;
				classAttrParts["whitespace-after"] = val.ToString(CultureInfo.InvariantCulture);
			}
		}
		#endregion
	}
}
