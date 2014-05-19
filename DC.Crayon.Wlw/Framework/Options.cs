using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace DC.Crayon.Wlw.Framework
{
	internal static class Options
	{
		#region Standard Items
		private static readonly StandardOptions _standardOptions = StandardOptions.GetFromResource();
		public static readonly ListItem[] StandardThemeListItems = _standardOptions.StandardThemeListItems;
		public static readonly ListItem[] StandardFontListItems = _standardOptions.StandardFontListItems;
		public static readonly ListItem[] StandardLanguageListItems = _standardOptions.StandardLanguageListItems;

		/// <summary>
		/// Global object to represent a default value
		/// </summary>
		public static readonly string Default = "__DEFAULT_VALUE__";
		#endregion

		#region All Options
		/// <summary>
		/// Plug-in options
		/// </summary>
		public static readonly Option[] AllOptions = new []
		{
			new CustomOptionTextOption { Group = "CustomOptions", Name = "Themes", DefaultValue = new ListItem[0], ApplicableToContent = false },
			new CustomOptionTextOption { Group = "CustomOptions", Name = "Languages", DefaultValue = new ListItem[0], ApplicableToContent = false },
			new CustomOptionTextOption { Group = "CustomOptions", Name = "Fonts", DefaultValue = new ListItem[0], ApplicableToContent = false },

			new ThemeNameOption { Group = "Theme", Name = "Name", DefaultValue = "classic" },

			new FontNameOption { Group = "Font", Name = "Name", DefaultValue = "monaco" },
			new FontSizeOption { Group = "Font", Name = "FontSize", DefaultValue = new FontSize() },

			new WidthOrHeightOption { Group = "Metrics", Name = "Width", DefaultValue = new WidthOrHeight() },
			new WidthOrHeightOption { Group = "Metrics", Name = "Height", DefaultValue = new WidthOrHeight() },
			new NumericOption { Group = "Metrics", Name = "TopMargin", DefaultValue = 12 },
			new NumericOption { Group = "Metrics", Name = "RightMargin", DefaultValue = 12 },
			new NumericOption { Group = "Metrics", Name = "BottomMargin", DefaultValue = 12 },
			new NumericOption { Group = "Metrics", Name = "LeftMargin", DefaultValue = 12 },
			new Option { Group = "Metrics", Name = "HorizontalAlignment", DefaultValue = HorizontalAlignment.None },
			new Option { Group = "Metrics", Name = "AllowFloatingElementsToSurroundCrayon", DefaultValue = false },
			new NumericOption { Group = "Metrics", Name = "InlineMargin", DefaultValue = 5 },

			new Option { Group = "Toolbar", Name = "Display", DefaultValue = ToolbarDisplay.OnMouseOver },
			new Option { Group = "Toolbar", Name = "OverlayOnCode", DefaultValue = true },
			new Option { Group = "Toolbar", Name = "ToggleOnSingleClickWhenOverlayed", DefaultValue = true },
			new Option { Group = "Toolbar", Name = "DelayHideOnMouseOut", DefaultValue = true },
			new Option { Group = "Toolbar", Name = "ShowTitle", DefaultValue = true },
			new Option { Group = "Toolbar", Name = "LanguageDisplay", DefaultValue = LanguageDisplay.WhenFound },

			new Option { Group = "Lines", Name = "StripedLines", DefaultValue = true },
			new Option { Group = "Lines", Name = "EnableLineMarking", DefaultValue = true },
			new Option { Group = "Lines", Name = "EnableLineRanges", DefaultValue = true },
			new Option { Group = "Lines", Name = "DisplayNumbersByDefault", DefaultValue = true },
			new Option { Group = "Lines", Name = "EnableLineNumberToggling", DefaultValue = true },
			new Option { Group = "Lines", Name = "WrapLinesByDefault", DefaultValue = false },
			new Option { Group = "Lines", Name = "EnableLineWrapToggling", DefaultValue = true },
			new NumericOption { Group = "Lines", Name = "StartingLineNumber", DefaultValue = 1, MinValue = 1 },

			new Option { Group = "Code", Name = "EnablePlainCodeView", DefaultValue = true },
			new Option { Group = "Code", Name = "PlainCodeViewDisplayTrigger", DefaultValue = PlainCodeViewDisplayMouseTrigger.DoubleClick },
			new Option { Group = "Code", Name = "EnablePlainCodeToggling", DefaultValue = true },
			new Option { Group = "Code", Name = "ShowPlainCodeByDefault", DefaultValue = false },
			new Option { Group = "Code", Name = "EnableCopyPaste", DefaultValue = true },
			new Option { Group = "Code", Name = "EnableOpenInNewWindow", DefaultValue = true },
			new Option { Group = "Code", Name = "AlwaysDisplayScrollbars", DefaultValue = false },
			new Option { Group = "Code", Name = "MinimizeCode", DefaultValue = false },
			new Option { Group = "Code", Name = "ExpandCodeBeyondPageBorderOnMouseHover", DefaultValue = false },
			new Option { Group = "Code", Name = "EnableCodeExpandingToggling", DefaultValue = true },
			new Option { Group = "Code", Name = "DecodeHtmlEntitiesInAttributes", DefaultValue = true },
			new Option { Group = "Code", Name = "RemoveWhitespacesSurroundingShortcodeContent", DefaultValue = true },
			new Option { Group = "Code", Name = "RemoveCodeTagsSurroundingShortcodeContent", DefaultValue = true },
			new Option { Group = "Code", Name = "AllowMixedLanguageHighlightingWithDelimitersAndTags", DefaultValue = true },
			new Option { Group = "Code", Name = "ShowMixedLanguageIcon", DefaultValue = true },
			new NumericOption { Group = "Code", Name = "TabSizeInSpaces", DefaultValue = 4, MinValue = 0 },
			new NumericOption { Group = "Code", Name = "BlankLinesBeforeCode", DefaultValue = 0, MinValue = 0 },
			new NumericOption { Group = "Code", Name = "BlankLinesAfterCode", DefaultValue = 0, MinValue = 0 }
		};

		/// <summary>
		/// Shortcut to Themes option
		/// </summary>
		public static readonly Option CustomThemesOption = AllOptions.SingleOrDefault(o => string.Equals(o.Group, "CustomOptions", StringComparison.Ordinal) && string.Equals(o.Name, "Themes", StringComparison.Ordinal));
		/// <summary>
		/// Shortcut to Languages option
		/// </summary>
		public static readonly Option CustomLanguagesOption = AllOptions.SingleOrDefault(o => string.Equals(o.Group, "CustomOptions", StringComparison.Ordinal) && string.Equals(o.Name, "Languages", StringComparison.Ordinal));
		/// <summary>
		/// Shortcut to Fonts option
		/// </summary>
		public static readonly Option CustomFontsOption = AllOptions.SingleOrDefault(o => string.Equals(o.Group, "CustomOptions", StringComparison.Ordinal) && string.Equals(o.Name, "Fonts", StringComparison.Ordinal));
		/// <summary>
		/// Shortcut to Width option
		/// </summary>
		public static readonly Option WidthOption = AllOptions.SingleOrDefault(o => string.Equals(o.Group, "Metrics", StringComparison.Ordinal) && string.Equals(o.Name, "Width", StringComparison.Ordinal));
		/// <summary>
		/// Shortcut to Height option
		/// </summary>
		public static readonly Option HeightOption = AllOptions.SingleOrDefault(o => string.Equals(o.Group, "Metrics", StringComparison.Ordinal) && string.Equals(o.Name, "Height", StringComparison.Ordinal));

		/// <summary>
		/// Content options
		/// </summary>
		public static readonly Option[] ContentOnlyOptions = new []
		{
			new Option { Group = "Content", Name = "Title", DefaultValue = string.Empty },
			new Option { Group = "Content", Name = "Inline", DefaultValue = false },
			new Option { Group = "Content", Name = "DontHighlight", DefaultValue = false },
			new LanguageOption { Group = "Content", Name = "Language", DefaultValue = "default" },
			new LineRangeOption { Group = "Content", Name = "LineRange", DefaultValue = string.Empty },
			new LineRangeOption { Group = "Content", Name = "MarkedLines", DefaultValue = string.Empty },
			new MultilineTextOption { Group = "Content", Name = "Content", DefaultValue = string.Empty, Height = 100, IsRequired = true },
			new Option { Group = "Content", Name = "Url", DefaultValue = string.Empty }
		};
		#endregion

		#region Helpers
		/// <summary>
		/// Checks if the value is the global default value
		/// </summary>
		/// <param name="val">Value to test</param>
		/// <returns>True if the value is the global default value. False otherwise</returns>
		public static bool IsDefault(object val)
		{
			var valText = val as string;
			return (valText != null) && string.Equals(valText, Default, StringComparison.Ordinal);
		}

		/// <summary>
		/// Clears all saved properties
		/// </summary>
		/// <param name="properties">Properties dictionary</param>
		public static void ClearProperties(IProperties properties)
		{
			properties.Names.ToList().ForEach(properties.Remove);
			properties.SubPropertyNames.ToList().ForEach(properties.RemoveSubProperties);
		}

		/// <summary>
		/// Updates the size of the content
		/// </summary>
		/// <param name="contentProperties">Content properties</param>
		/// <param name="newSize">New size</param>
		public static void UpdateContentSize(IProperties contentProperties, Size newSize)
		{
			Options.WidthOption.SetValue(contentProperties, new WidthOrHeight()
			{
				Type = WidthOrHeightType.Static,
				Value = newSize.Width,
				Unit = WidthOrHeightUnit.Pixels
			});
			Options.HeightOption.SetValue(contentProperties, new WidthOrHeight()
			{
				Type = WidthOrHeightType.Static,
				Value = newSize.Height,
				Unit = WidthOrHeightUnit.Pixels
			});
		}

		/// <summary>
		/// Converts Properties dictionary into a string for debugging purposes
		/// </summary>
		/// <param name="properties">Properties</param>
		/// <returns>String</returns>
		public static object PropertiesInfo(IProperties properties)
		{
			var sbBuffer = new StringBuilder();
			using (var strWriter = new StringWriter(sbBuffer, CultureInfo.InvariantCulture))
			using (var textWriter = new IndentedTextWriter(strWriter, "\t"))
			{
				textWriter.NewLine = Environment.NewLine;
				PropertiesInfo(properties, textWriter);
			}
			return sbBuffer.ToString();
		}

		/// <summary>
		/// Converts Properties dictionary into a string for debugging purposes
		/// </summary>
		/// <param name="properties">Properties</param>
		/// <param name="textWriter">Text writer</param>
		/// <returns>String</returns>
		private static void PropertiesInfo(IProperties properties, IndentedTextWriter textWriter)
		{
			foreach (string name in properties.Names)
			{
				string val = properties.GetString(name, null) ?? "<null>";
				try
				{
					object oVal = Utils.BinaryDeserialize(Convert.FromBase64String(val));
					val = oVal.ToString();
				}
				catch { }
				textWriter.WriteLine("{0}: {1}", name, val);
			}
			foreach (string name in properties.SubPropertyNames)
			{
				textWriter.WriteLine("{0}:", name);
				textWriter.Indent += 1;
				PropertiesInfo(properties.GetSubProperties(name), textWriter);
				textWriter.Indent -= 1;
			}
		}

		/// <summary>
		/// Retrieves a list of all effective options
		/// </summary>
		/// <param name="contentProperties">Content properties dictionary</param>
		/// <param name="options">Plugin options dictionary</param>
		/// <returns>Effective options</returns>
		public static EffectiveOption[] GetEffectiveOptions(IProperties contentProperties, IProperties options)
		{
			// Standard options
			return ContentOnlyOptions.Union(AllOptions)
				.Select(o =>
				{
					var effectiveOption = new EffectiveOption();
					Utils.CopyProperties(o, effectiveOption);

					// Try from Content Properties, if this property is available
					if (o.HasValue(contentProperties))
					{
						effectiveOption.Value = o.GetValue(contentProperties);
						effectiveOption.Location = OptionLocation.Content;
						if (IsDefault(effectiveOption.Value))
						{
							effectiveOption.Location = OptionLocation.Default;
						}
					}
					else if (o.HasValue(options))
					{
						effectiveOption.Value = o.GetValue(options);
						effectiveOption.Location = OptionLocation.Plugin;
					}
					else
					{
						effectiveOption.Value = o.DefaultValue;
						effectiveOption.Location = OptionLocation.Default;
					}
					return effectiveOption;
				}).ToArray();
		}
		#endregion
	}
}
