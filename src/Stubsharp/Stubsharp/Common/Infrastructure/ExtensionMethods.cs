using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        /// <summary>
        /// Replaces one or more format items in a string and returns the result as a URI.
        /// </summary>
        /// <param name="pattern">The uri pattern</param>
        /// <param name="args">The pattern arguments</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UriFormatException"></exception>
        public static Uri FormatUri(this string pattern, params object[] args)
        {
            Guard.IsNotNullOrEmpty(pattern, "pattern");

            return new Uri(string.Format(CultureInfo.InvariantCulture, pattern, args), UriKind.Relative);
        }

        /// <summary>
        /// Applies the dictionary of parameters to the uri.  Any existing parameters on the uri will
        /// be merged with the provided parameters taking precedence.
        /// </summary>
        /// <param name="uri">Original request Uri</param>
        /// <param name="parameters">Collection of key-value pairs</param>
        /// <returns>Updated request Uri</returns>
        public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            Guard.IsNotNull(uri, "uri");

            if (parameters == null || parameters.Count == 0)
            {
                return uri;
            }

            var (baseUrl, queryParameters) = SplitUri(uri);

            var values = queryParameters.Split(new[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);

            var existingParameters = values.ToDictionary(
                key => key.Substring(0, key.IndexOf('=')),
                value => value.Substring(value.IndexOf('=') + 1)
            );

            IDictionary<string, string> p = new Dictionary<string, string>(existingParameters);

            foreach ( var parameter in parameters )
            {
                p.SetOrUpdate(parameter.Key, parameter.Value);
            }

            var query = string.Join("&", p.Select(kvp => kvp.Key + "=" + Uri.EscapeDataString(kvp.Value)));

            if (uri.IsAbsoluteUri)
            {
                return new UriBuilder(uri)
                {
                    Query = query
                }.Uri;
            }

            return new Uri(baseUrl + "?" + query, UriKind.Relative);
        }

        public static Uri RemoveParameter(this Uri uri, string key)
            => ApplyParameters(uri, new Dictionary<string, string> {{key, null}});

        /// <summary>
        /// Splits a uri into two parts, the base url and the query parameters
        /// </summary>
        /// <example>
        /// https://www.example.com/list?filter=value will return
        /// {
        ///     baseUrl: "https://www.example.com/list",
        ///     queryParameters: "?filter=value"
        /// }
        /// </example>
        /// <param name="uri">The uri to split</param>
        private static (string baseUrl, string queryParameters) SplitUri(Uri uri)
        {
            var parts = uri.OriginalString.Split(new[] {'?'}, StringSplitOptions.None);

            var q = parts.Length > 1 ? parts[1] : "";

            return (parts[0], q);
        }

        /// <summary>
        /// Sets the value in the collection if the string contains a value. If the value is null, then 
        /// the key will be removed from the collection.
        /// </summary>
        /// <param name="collection">The name value collection</param>
        /// <param name="key">They collection key</param>
        /// <param name="value">The value</param>
        public static void SetOrUpdate(this IDictionary<string, string> collection, string key, string value)
        {
            if (value == null)
            {
                collection.Remove(key);
            }
            else
            {
                collection[key] = value;
            }
        }

        /// <summary>
        /// Syntactic sugar for creating a TimeSpan of n seconds
        /// </summary>
        /// <param name="seconds">The # of seconds.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Seconds(this int seconds) =>
            new TimeSpan(TimeSpan.TicksPerSecond * seconds);

        /// <summary>
        /// Syntactic sugar for creating a TimeSpan of n minutes
        /// </summary>
        /// <param name="minutes">The # of minutes.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Minutes(this int minutes) =>
            new TimeSpan(TimeSpan.TicksPerMinute * minutes);

        /// <summary>
        /// Syntactic sugar for creating a TimeSpan of n hours
        /// </summary>
        /// <param name="hours">The # of hours.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Hours(this int hours) =>
            new TimeSpan(TimeSpan.TicksPerHour * hours);

        /// <summary>
        /// Syntactic sugar for creating a TimeSpan of n days
        /// </summary>
        /// <param name="days">The # of days.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan Days(this int days) =>
            new TimeSpan(TimeSpan.TicksPerDay * days);
    }
}
