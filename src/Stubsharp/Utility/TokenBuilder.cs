using System;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Stubsharp.Tests")]
namespace Stubsharp.Utility
{
    internal static class TokenBuilder
    {
        public static string CreateAuthorizationToken(string apiKey, string appSecret)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            if (appSecret == null)
            {
                throw new ArgumentNullException(nameof(appSecret));
            }

            var baseToken = $"{apiKey}:{appSecret}";

            byte[] encodedBaseToken = Encoding.ASCII.GetBytes(baseToken);

            return Convert.ToBase64String(encodedBaseToken);
        }
    }
}
