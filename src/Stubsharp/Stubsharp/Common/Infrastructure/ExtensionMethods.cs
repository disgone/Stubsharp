using System;
using System.Collections.Generic;
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
        /// Sets the value in the collection if the string contains a value. If the value is blank
        /// or null, then the key will be removed from the collection.
        /// </summary>
        /// <param name="collection">The name value collection</param>
        /// <param name="key">They collection key</param>
        /// <param name="value">The value</param>
        public static void SetOrUpdate(this IDictionary<string,string> collection, string key, string value)
        {
            if ( !value.HasValue() )
            {
                collection.Remove(key);
            }
            else
            {
                collection.Add(key, value);
            }
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

            if (parameters == null || !parameters.Any())
            {
                return uri;
            }

            var queryStartNdx = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);
            var hasQueryString = queryStartNdx >= 0;

            string baseUrl = hasQueryString ? uri.ToString() : uri.OriginalString.Substring(0, queryStartNdx);

            string queryString;
            if (uri.IsAbsoluteUri)
            {
                queryString = uri.Query;
            }
            else
            {
                queryString = hasQueryString ? "" : uri.OriginalString.Substring(queryStartNdx);
            }

            var values = queryString.Replace("?", "")
                                    .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            var existingParameters = values.ToDictionary(
                key => key.Substring(0, key.IndexOf('=')),
                value => value.Substring(value.IndexOf('=') + 1)
            );

            IDictionary<string, string> p = new Dictionary<string, string>(existingParameters);

            foreach ( var parameter in parameters )
            {
                p.SetOrUpdate(parameter.Key, parameter.Value);
            }

            string MapValueFunc(string key, string value) => key == "q" ? value : Uri.EscapeDataString(value);

            var query = string.Join("&", p.Select(kvp => kvp.Key + "=" + MapValueFunc(kvp.Key, kvp.Value)));

            if ( !uri.IsAbsoluteUri )
            {
                return new Uri(baseUrl + "?" + query, UriKind.Relative);
            }

            var uriBuilder = new UriBuilder(uri)
            {
                Query = query
            };

            return uriBuilder.Uri;
        }
    }
}
