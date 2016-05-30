using System;
using System.Text;

namespace Stubsharp.Utility
{
    public static class TokenBuilder
    {
        public static string CreateAuthorizationToken(string apiKey, string appSecret)
        {
            var baseToken = $"{apiKey}:{appSecret}";
            byte[] encodedBaseToken = Encoding.ASCII.GetBytes(baseToken);
            return Convert.ToBase64String(encodedBaseToken);
        }
    }
}
