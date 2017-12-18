using System;
using System.Net;
using System.Text;

namespace Stubsharp.Common.Infrastructure
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Determines whether the provided input string has a value (not null or empty).
        /// </summary>
        /// <remarks>Lazy mans equivalent for !string.IsNullOrWhiteSpace</remarks>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input has value; otherwise, <c>false</c>.</returns>
        public static bool HasValue(this string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Uri encodes the input string
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string UriEncode(this string input)
        {
            return WebUtility.UrlEncode(input);
        }

        /// <summary>
        /// Encodes the string to a base 64 string
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string ToBase64(this string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Decodes the base64 back to an ansi string
        /// </summary>
        /// <param name="encoded">The encoded.</param>
        /// <returns>System.String.</returns>
        public static string FromBase64(this string encoded)
        {
            var decodedBytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(decodedBytes, 0, decodedBytes.Length);
        }
    }
}
