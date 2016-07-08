using System;
using System.Net.Http;

namespace Stubsharp
{
    public interface IHttpClientFactory : IDisposable
    {
        /// <summary>
        /// Gets a basic client
        /// </summary>
        /// <param name="environment">The StubHub environment</param>
        /// <returns></returns>
        HttpClient GetClient(StubhubEnvironment environment);

        /// <summary>
        /// Gets a client with a bearer auth token
        /// </summary>
        /// <param name="environment">The StubHub environment</param>
        /// <param name="authenticationToken">The bearer token</param>
        /// <returns></returns>
        HttpClient GetAuthenticatedClient(StubhubEnvironment environment, string authenticationToken);
    }
}