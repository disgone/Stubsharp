using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Stubsharp
{
    public class HttpClientFactory : IHttpClientFactory
    {
        /// <summary>
        /// Gets a basic client
        /// </summary>
        /// <param name="environment">The StubHub environment</param>
        /// <returns></returns>
        public HttpClient GetClient(StubhubEnvironment environment)
        {
            return _basicClient ?? (_basicClient = CreateClientInstance(environment));
        }

        /// <summary>
        /// Gets a client with a bearer auth token
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public HttpClient GetAuthenticatedClient(StubhubEnvironment environment, string authenticationToken)
        {
            if (_authedClient == null)
            {
                _authedClient = CreateClientInstance(environment);
                _authedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            }
            return _authedClient;
        }

        protected HttpClient CreateClientInstance(StubhubEnvironment environment)
        {
            var baseUri = new Uri($"https://{environment.Domain}/");

            HttpClient client = new HttpClient
            {
                BaseAddress = baseUri
            };

            return client;
        }

        private static HttpClient _basicClient;
        private static HttpClient _authedClient;
        public void Dispose()
        {
            _basicClient?.Dispose();
            _authedClient?.Dispose();
        }
    }
}