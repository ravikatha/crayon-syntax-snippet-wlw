using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
		public static StandardOptions GetFromResource()
		{
			Assembly resourceAssembly = Assembly.GetExecutingAssembly();
			const string resourceName = "DC.Crayon.Wlw.Framework.StandardOptions.bin";
			byte[] data = null;

			// Read All Bytes from the resource
			using (var resourceStream = resourceAssembly.GetManifestResourceStream(resourceName))
			{
				if (resourceStream != null)
				{
					const int bufferLength = 65536;
					var dataBuffer = new byte[bufferLength];
					using (var memStream = new MemoryStream())
					{
						while (true)
						{
							var readBytes = resourceStream.Read(dataBuffer, 0, bufferLength);
							if (readBytes == 0)
							{
								break;
							}
							memStream.Write(dataBuffer, 0, readBytes);
						}
						data = memStream.ToArray();
					}
				}
			}

			// Deserialize
			return BinaryDeserialize(data);
		}

		/// <summary>
		/// Serializes the specified object using a binary serializer
		/// </summary>
		/// <param name="surrogateSelector">Surrogate selector</param>
		/// <returns>Byte array</returns>
		public byte[] BinarySerialize(ISurrogateSelector surrogateSelector = null)
		{
			var binFmtr = new BinaryFormatter();
			if (surrogateSelector != null)
			{
				binFmtr.SurrogateSelector = surrogateSelector;
			}
			using (var memStream = new MemoryStream())
			{
				binFmtr.Serialize(memStream, this);
				return memStream.ToArray();
			}
		}

		/// <summary>
		/// Deserialize a byte buffer (serialized by BinarySerialize) to its corresponding instance
		/// </summary>
		/// <param name="sourceInstanceData"></param>
		/// <param name="surrogateSelector">Surrogate selector</param>
		/// <returns>Deserialized object</returns>
		private static StandardOptions BinaryDeserialize(byte[] sourceInstanceData, ISurrogateSelector surrogateSelector = null)
		{
			if ((sourceInstanceData == null) || (sourceInstanceData.Length == 0))
			{
				return null;
			}

			var binFmtr = new BinaryFormatter();
			if (surrogateSelector != null)
			{
				binFmtr.SurrogateSelector = surrogateSelector;
			}
			using (var memStream = new MemoryStream(sourceInstanceData))
			{
				return binFmtr.Deserialize(memStream) as StandardOptions;
			}
		}
	}
}
