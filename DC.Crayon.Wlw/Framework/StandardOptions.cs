using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Xml;

namespace DC.Crayon.Wlw.Framework
{
	[Serializable]
	public sealed class StandardOptions
	{
		public ListItem[] StandardThemeListItems;
		public ListItem[] StandardFontListItems;
		public ListItem[] StandardLanguageListItems;

		/// <summary>
		/// Reads the options object from the resource file
		/// </summary>
		/// <returns>Standard Options</returns>
		internal static StandardOptions GetFromResource()
		{
			Assembly resourceAssembly = Assembly.GetExecutingAssembly();
			const string resourceName = "DC.Crayon.Wlw.Framework.StandardOptions.xml";

			// Read All Bytes from the resource
			using (var resourceStream = resourceAssembly.GetManifestResourceStream(resourceName))
			{
				if (resourceStream != null)
				{
					using (var resourceReader = new StreamReader(resourceStream, Encoding.UTF8, true))
					{
						return resourceReader.ReadToEnd().XmlDeserialize() as StandardOptions;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Converts this instance to XML
		/// </summary>
		/// <returns></returns>
		public string ConvertToXml()
		{
			return this.XmlSerialize(ConformanceLevel.Document, true);
		}
	}
}
