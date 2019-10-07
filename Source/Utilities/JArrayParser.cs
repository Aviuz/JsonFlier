using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JsonFlier.Utilities
{
    public static class JArrayParser
    {
        public static IEnumerable<JObject> Parse(Stream stream, Encoding encoding)
        {
            using (var reader = new StreamReader(stream, encoding))
            {
                char charIterator;
                var stringBuilder = new StringBuilder();

                // Skip some whitespaces
                do
                {
                    charIterator = (char)reader.Read();
                }
                while (char.IsWhiteSpace(charIterator) && !reader.EndOfStream);

                // Check if there is symbol for opening array
                if (charIterator != '[')
                {
                    throw new FormatException("Invalid JSON format");
                }

                // Begin iterating
                while (!reader.EndOfStream)
                {
                    // Skip some whitespaces
                    do
                    {
                        charIterator = (char)reader.Read();
                    }
                    while (char.IsWhiteSpace(charIterator) && !reader.EndOfStream);

                    // Parse object
                    if (charIterator == '{')
                    {
                        int curLevel = 1;
                        stringBuilder.Clear();
                        stringBuilder.Append(charIterator);

                        while (curLevel > 0 && !reader.EndOfStream)
                        {
                            charIterator = (char)reader.Read();
                            stringBuilder.Append(charIterator);

                            if (charIterator == '{')
                                curLevel++;
                            if (charIterator == '}')
                                curLevel--;
                        }

                        if (reader.EndOfStream)
                            throw new FormatException("Unexpected end of stream");

                        yield return JObject.Parse(stringBuilder.ToString());
                    }
                    // Coma, we just ignore them
                    else if (charIterator == ',')
                    {
                        // do nothing
                    }
                    // End of array, let algorithm stop
                    else if (charIterator == ']')
                    {
                        break;
                    }
                    // Other unexpected character
                    else
                    {
                        throw new FormatException($"Invalid JSON format, unexpected token {charIterator}");
                    }
                }
            }
        }

        public static bool TryParse(Stream stream, Encoding encoding, out IEnumerable<JObject> objects)
        {
            try
            {
                objects = Parse(stream, encoding).ToList();
                return true;
            }
            catch (FormatException)
            {
                objects = null;
                return false;
            }
        }
    }
}
