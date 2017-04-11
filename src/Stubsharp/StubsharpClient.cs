using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Stubsharp.Models.Common.Request;
using Stubsharp.Models.EventSearch.Request;
using Stubsharp.Models.EventSearch.Response;
using Stubsharp.Models.InventorySearch.Request;
using Stubsharp.Models.InventorySearch.Response;
using Stubsharp.Utility;
using Stubsharp.Utility.Serializers;

namespace Stubsharp
{
    public class StubsharpClient : IDisposable
    {
        

        public StubsharpClient(string apiKey, string apiSecret, StubhubEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("API key is required");
            }

            if (string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentException("API secret is required");
            }

            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _environment = environment ?? StubhubEnvironment.Sandbox;

            HttpClientFactory = new HttpClientFactory();

            Serializer = new JsonNetSerializer();
        }

        /// <summary>
        /// Flag indicating if the client has been authenticated.
        /// </summary>
        /// <returns>True if authenticated, false otherwise</returns>
        public bool IsAuthenticated()
        {
            return _authentication != null;
        }

        /// <summary>
        /// Sets the access and refresh tokens to use with the client.
        /// </summary>
        /// <param name="accessToken">The access token</param>
        /// <param name="refreshToken">The refresh token</param>
        public void SetAuthenticationInformation(string accessToken, string refreshToken)
        {
            _authentication = new StubsharpAuthentication
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// The serializer used to deserialize the http response
        /// </summary>
        public ISerializer Serializer { get; set; }

        /// <summary>
        /// The factor to create an HttpClient
        /// </summary>
        public IHttpClientFactory HttpClientFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequest">The login credentials</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task<StubsharpAuthentication> Login(LoginRequest loginRequest,CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = HttpClientFactory.GetClient(_environment);

            IEnumerable<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", loginRequest.UserName),
                new KeyValuePair<string, string>("password", loginRequest.Password),
                new KeyValuePair<string, string>("scope", _environment.Name),
            };

            var request = new HttpRequestMessage(HttpMethod.Post, StubhubEndpoint.Login.Url)
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Basic",
                            $"{TokenBuilder.CreateAuthorizationToken(_apiKey, _apiSecret)}")
                },
                Content = new FormUrlEncodedContent(data)
            };

            var response = await SendRequest(client, request, cancellationToken).ConfigureAwait(false);

            try
            {
                var auth = await HandleResponse<StubsharpAuthentication>(response, cancellationToken);

                string userId = response.Headers.GetValues("X-StubHub-User-GUID")?.FirstOrDefault();

                if (!string.IsNullOrEmpty(userId)) auth.UserId = new Guid(userId);

                _authentication = auth;

                return auth;
            }
            catch( WebException e )
            {
                throw new Exception("Authentication failed", e);
            }
        }

        /// <summary>
        /// Submits an event search request
        /// </summary>
        public async Task<EventSearchResponse> SearchEvents(EventSearchRequest eventSearchRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await InvokeEndpoint(StubhubEndpoint.EventSearchV3, eventSearchRequest,cancellationToken);

            return await HandleResponse<EventSearchResponse>(response, cancellationToken);
        }

        /// <summary>
        /// Submits an inventory search request
        /// </summary>
        public async Task<InventorySearchResponse> SearchInventory(InventorySearchRequest inventorySearchRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await InvokeEndpoint(StubhubEndpoint.InventorySearchV2, inventorySearchRequest, cancellationToken);

            return await HandleResponse<InventorySearchResponse>(response, cancellationToken);
        }

        /// <summary>
        /// Invokes the search endpoint
        /// </summary>
        /// <param name="endpoint">The endpoint to target</param>
        /// <param name="request">The search request filters</param>
        /// <param name="cancellationToken">The cancellation to ken</param>
        private Task<HttpResponseMessage> InvokeEndpoint(StubhubEndpoint endpoint, SearchRequestBase request, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (endpoint.RequiresAuthorization && !IsAuthenticated())
            {
                throw new InvalidOperationException($"{endpoint.Name} requires authentication");
            }

            var client = HttpClientFactory.GetAuthenticatedClient(_environment, _authentication.AccessToken);

            var httpRequest = CreateSearchRequest(endpoint, request);

            return SendRequest(client, httpRequest, cancellationToken);
        }

        /// <summary>
        /// Creates an http search request
        /// </summary>
        /// <param name="endpoint">The endpoint to submit the search request</param>
        /// <param name="seachRequest">The search filters</param>
        private HttpRequestMessage CreateSearchRequest(StubhubEndpoint endpoint, SearchRequestBase seachRequest)
        {
            return new HttpRequestMessage(HttpMethod.Get, $"{endpoint.Url}?{seachRequest.ToQueryString()}");
        }

        /// <summary>
        /// Submits a http request
        /// </summary>
        /// <param name="client">The client to use to submit the request</param>
        /// <param name="request">The http request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        private Task<HttpResponseMessage> SendRequest(
            HttpClient client, 
            HttpRequestMessage request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return client.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Deserialize the http response
        /// </summary>
        /// <typeparam name="T">The output type</typeparam>
        /// <param name="response">The http response</param>
        /// <param name="cancellationToken">The cancellation token</param>
        private async Task<T> HandleResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default(CancellationToken))
        {
            response.EnsureSuccessStatusCode();

            cancellationToken.ThrowIfCancellationRequested();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return Serializer.Deserialize<T>(responseStream);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                HttpClientFactory.Dispose();
            }
            _disposed = true;
        }

        private readonly StubhubEnvironment _environment;

        private StubsharpAuthentication _authentication;

        private readonly string _apiKey;

        private readonly string _apiSecret;

        private bool _disposed;
    }
}
