using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DC.Crayon.Wlw
{
	/// <summary>
	/// Utilities
	/// </summary>
	internal static class Utils
	{
		private delegate bool CheckSplittableDelegate(int i);

		public static string NameToDisplayName(string name, string wordSeparator = null, bool convertToTitleCase = false, IEnumerable<string> nonSplittableTokens = null)
		{
			// Check name
			name = (name ?? string.Empty).Trim();
			if (name.Length == 0)
			{
				return name;
			}
			name = name.Trim();

			// Adjust word separator
			wordSeparator = wordSeparator ?? " ";

			// Non splittable token indices
			List<Point> nonSplittableTokenIndices = new List<Point>();
			if (nonSplittableTokens != null)
			{
				List<string> distinctNonSplittableTokens = new List<string>();
				foreach (string nonSplittableToken in nonSplittableTokens)
				{
					if (distinctNonSplittableTokens.Find(delegate(string str) { return string.Equals(str, nonSplittableToken, StringComparison.OrdinalIgnoreCase); }) == null)
					{
						distinctNonSplittableTokens.Add(nonSplittableToken);
					}
				}
				foreach (string nonSplittableToken in distinctNonSplittableTokens)
				{
					int index = name.IndexOf(nonSplittableToken, StringComparison.OrdinalIgnoreCase);
					if (index >= 0)
					{
						nonSplittableTokenIndices.Add(new Point(index, index + nonSplittableToken.Length - 1));
					}
				}
			}
			CheckSplittableDelegate checkSplittable = delegate(int i)
			{
				bool prvCharInRange = false;
				foreach (Point item in nonSplittableTokenIndices)
				{
					if ((item.X <= (i - 1)) && ((i - 1) <= item.Y))
					{
						prvCharInRange = true;
						break;
					}
				}

				bool thisCharInRange = false;
				foreach (Point item in nonSplittableTokenIndices)
				{
					if ((item.X <= i) && (i <= item.Y))
					{
						thisCharInRange = true;
						break;
					}
				}

				return !(prvCharInRange && thisCharInRange);
			};

			StringBuilder sbBuffer = new StringBuilder();
			int prvCharClass = 0; // 0 => whitespace, 1 => lowercase, 2 => uppercase, 3 => others
			int curCharClass;
			bool isFirstChar = true;
			char[] charArray = name.ToCharArray();

			for (int iChar = 0, nChars = charArray.Length; iChar < nChars; ++iChar)
			{
				char ch = charArray[iChar];

				// Determine character class
				if (char.IsWhiteSpace(ch))
				{
					curCharClass = 0;
				}
				else if (char.IsLetter(ch))
				{
					curCharClass = char.IsLower(ch) ? 1 : 2;
				}
				else
				{
					curCharClass = 3;
				}

				if (isFirstChar)
				{
					sbBuffer.Append(ch);
					isFirstChar = false;
				}
				else
				{
					// Whitespace
					if (curCharClass == 0)
					{
						// Append only if prv is not whitespace
						if ((prvCharClass != 0) && checkSplittable(iChar))
						{
							sbBuffer.Append(wordSeparator);
						}
					}
					// Lowercase
					else if (curCharClass == 1)
					{
						// Append separator if previous char is other
						if ((prvCharClass == 3) && checkSplittable(iChar))
						{
							sbBuffer.Append(wordSeparator);

							// Convert to upper case if required
							if (convertToTitleCase)
							{
								ch = Char.ToUpperInvariant(ch);
							}
						}
						sbBuffer.Append(ch);
					}
					// Uppercase
					else if (curCharClass == 2)
					{
						// Append separator if previous char is lowercase or other
						if ((prvCharClass == 1) || (prvCharClass == 3))
						{
							if (checkSplittable(iChar))
							{
								sbBuffer.Append(wordSeparator);
							}
						}
						// Append separator if this character is starting of a word
						else if (prvCharClass == 2)
						{
							// Check the next character
							if ((iChar < (nChars - 1))
								&& char.IsLower(charArray[iChar + 1])
								&& checkSplittable(iChar))
							{
								sbBuffer.Append(wordSeparator);
							}
						}
						sbBuffer.Append(ch);
					}
					// Other
					else if (curCharClass == 3)
					{
						// Append separator if previous char is anything other than other and whitespace
						if ((prvCharClass != 0) && (prvCharClass != 3) && checkSplittable(iChar))
						{
							sbBuffer.Append(wordSeparator);
						}
						sbBuffer.Append(ch);
					}
				}
				prvCharClass = curCharClass;
			}

			if (!convertToTitleCase)
			{
				string text = sbBuffer.ToString();
				return text.Substring(0, 1).ToUpperInvariant() + text.Substring(1).ToLowerInvariant();
			}
			return sbBuffer.ToString();
		}

		/// <summary>
		/// Serializes the specified object using a binary serializer
		/// </summary>
		/// <param name="sourceInstance">Instance to be serialized</param>
		/// <param name="surrogateSelector">Surrogate selector</param>
		/// <returns>Byte array</returns>
		public static byte[] BinarySerialize(object sourceInstance, ISurrogateSelector surrogateSelector = null)
		{
			if (sourceInstance == null)
			{
				return null;
			}

			var binFmtr = new BinaryFormatter();
			if (surrogateSelector != null)
			{
				binFmtr.SurrogateSelector = surrogateSelector;
			}
			using (var memStream = new MemoryStream())
			{
				binFmtr.Serialize(memStream, sourceInstance);
				return memStream.ToArray();
			}
		}

		/// <summary>
		/// De-serialize a byte buffer (serialized by BinarySerialize) to its corresponding instance
		/// </summary>
		/// <param name="sourceInstanceData"></param>
		/// <param name="surrogateSelector">Surrogate selector</param>
		/// <returns>De-serialized object</returns>
		public static object BinaryDeserialize(byte[] sourceInstanceData, ISurrogateSelector surrogateSelector = null)
		{
			if ((sourceInstanceData == null) ||
				(sourceInstanceData.Length == 0))
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
				return binFmtr.Deserialize(memStream);
			}
		}

		/// <summary>
		/// Copies properties with same name from the source to the target
		/// (Quick fix)
		/// </summary>
		/// <param name="source">Source instance</param>
		/// <param name="target">Target instance</param>
		public static void CopyProperties(object source, object target)
		{
			PropertyDescriptor[] srcProps = TypeDescriptor.GetProperties(source).Cast<PropertyDescriptor>().ToArray();
			PropertyDescriptor[] targetProps = TypeDescriptor.GetProperties(target).Cast<PropertyDescriptor>().ToArray();
			foreach (PropertyDescriptor targetProp in targetProps)
			{
				PropertyDescriptor srcProp = srcProps.FirstOrDefault(p => string.Equals(p.Name, targetProp.Name, StringComparison.Ordinal) && (targetProp.PropertyType == p.PropertyType));
				if (srcProp != null)
				{
					targetProp.SetValue(target, srcProp.GetValue(source));
				}
			}
		}
	}
}
