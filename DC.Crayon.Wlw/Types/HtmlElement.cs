using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace DC.Crayon.Wlw
{
	internal class HtmlElement
	{
		#region Properties
		private string _tag = "div";
		/// <summary>
		/// Tag name
		/// </summary>
		public string Tag
		{
			get { return _tag; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_tag = (val.Length == 0) ? "div" : val.ToLowerInvariant();
			}
		}

		private bool _canSelfClose = true;
		/// <summary>
		/// Can this element self-close
		/// </summary>
		public bool CanSelfClose
		{
			get { return _canSelfClose; }
			set { _canSelfClose = value; }
		}

		private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		/// <summary>
		/// Attributes
		/// </summary>
		public Dictionary<string, string> Attributes
		{
			get { return _attributes; }
		}

		private readonly List<HtmlElement> _children = new List<HtmlElement>();
		/// <summary>
		/// Children
		/// </summary>
		public List<HtmlElement> Children
		{
			get { return _children; }
		}

		private string _text;
		/// <summary>
		/// Text content
		/// </summary>
		public string Text
		{
			get { return _text; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_text = (val.Length == 0) ? null : val;
			}
		}

		private string _html;
		/// <summary>
		/// Html content
		/// </summary>
		public string Html
		{
			get { return _html; }
			set
			{
				string val = (value ?? string.Empty).Trim();
				_html = (val.Length == 0) ? null : val;
			}
		}
		#endregion

		#region Overrides
		public override string ToString()
		{
			StringBuilder sbBuffer = new StringBuilder();

			// Opening Tag
			sbBuffer.AppendFormat(CultureInfo.InvariantCulture, "<{0}", Tag);

			// Attributes
			Attributes.ToList()
				.ForEach(a => sbBuffer.AppendFormat(CultureInfo.InvariantCulture, " {0}=\"{1}\"", a.Key, EscapeAttributeValue(a.Value)));

			// Children count
			List<HtmlElement> childElements = Children.Where(c => c != null).ToList();
			if ((childElements.Count == 0) && (Text == null) && (Html == null) && CanSelfClose)
			{
				// Self close & return
				sbBuffer.Append(" />");
				return sbBuffer.ToString();
			}

			// Close opening tag
			sbBuffer.Append(">");

			// Text
			if (Text != null)
			{
				sbBuffer.Append(EscapeHtml(Text));
			}

			// Html
			if (Html != null)
			{
				sbBuffer.Append(Html);
			}

			// Append children
			childElements.ForEach(c => sbBuffer.Append(c.ToString()));

			// End tag
			sbBuffer.AppendFormat(CultureInfo.InvariantCulture, "</{0}>", Tag);

			// Return
			return sbBuffer.ToString();
		}
		#endregion

		#region Helpers
		/// <summary>
		/// Escapes attribute value
		/// </summary>
		/// <param name="value">Value</param>
		/// <returns>Escaped value</returns>
		public static string EscapeAttributeValue(string value)
		{
			return HttpUtility.HtmlAttributeEncode(value);
		}

		/// <summary>
		/// Escapes html content
		/// </summary>
		/// <param name="value">Value</param>
		/// <returns>Escaped value</returns>
		public static string EscapeHtml(string value)
		{
			return HttpUtility.HtmlEncode(value);
		}
		#endregion
	}
}
