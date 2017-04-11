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
            return _basicClient ?? (_basicClient = CreateInstance(environment));
        }

        /// <summary>
        /// Gets a client with a bearer auth token
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="authenticationToken"></param>
        /// <returns></returns>
        public HttpClient GetAuthenticatedClient(StubhubEnvironment environment, string authenticationToken)
        {
            if (_authedClient != null) return _authedClient;

            if (string.IsNullOrWhiteSpace(authenticationToken))
                throw new ArgumentException("Invalid authentication token", nameof(authenticationToken));

            _authedClient = CreateInstance(environment);
            _authedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            return _authedClient;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected HttpClient CreateInstance(StubhubEnvironment environment)
        {
            var baseUri = new Uri($"https://{environment.Domain}/");

            HttpClient client = new HttpClient
            {
                BaseAddress = baseUri
            };

            return client;
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _basicClient?.Dispose();
                _authedClient?.Dispose();
            }
            _disposed = true;
        }

        private static HttpClient _basicClient;

        private static HttpClient _authedClient;

        private bool _disposed;
    }
}