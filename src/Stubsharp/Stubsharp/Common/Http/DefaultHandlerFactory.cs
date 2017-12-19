using System.Net;
using System.Net.Http;

namespace Stubsharp.Common.Http
{
    internal static class DefaultHandlerFactory
    {
        internal static HttpMessageHandler Create()
        {
            return Create(null);
        }

        internal static HttpMessageHandler Create(IWebProxy proxy)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
#if !PORTABLE
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            if (handler.SupportsProxy && proxy != null)
            {
                handler.UseProxy = true;
                handler.Proxy = proxy;
            }
#endif
            return handler;
        }
    }
}
