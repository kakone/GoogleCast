using System;
using System.Text;

namespace GoogleCast
{
    /// <summary>
    /// Extensions methods for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a camel case string to underscore notation
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>the converted string to underscore notation</returns>
        public static string ToUnderscoreUpperInvariant(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var stringBuilder = new StringBuilder();
            var first = true;
            foreach (var c in str)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    if (Char.IsUpper(c))
                    {
                        stringBuilder.AppendFormat("_{0}", c);
                        continue;
                    }
                }
                stringBuilder.Append(Char.ToUpperInvariant(c));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a string to camel case notation
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>camel case notation</returns>
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var stringBuilder = new StringBuilder();
            var underscore = true;
            foreach (var c in str)
            {
                if (underscore)
                {
                    underscore = false;
                    stringBuilder.Append(Char.ToUpperInvariant(c));
                }
                else
                {
                    if (c == '_')
                    {
                        underscore = true;
                    }
                    else
                    {
                        stringBuilder.Append(Char.ToLowerInvariant(c));
                    }
                }
            };
            return stringBuilder.ToString();
        }
    }
}
