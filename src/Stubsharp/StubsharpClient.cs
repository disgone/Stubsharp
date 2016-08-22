using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stubsharp.Models.Common.Request;
using Stubsharp.Models.EventSearch.Request;
using Stubsharp.Models.EventSearch.Response;
using Stubsharp.Models.InventorySearch.V1.Request;
using Stubsharp.Models.InventorySearch.V1.Response;
using Stubsharp.Utility;

namespace Stubsharp
{
    public class StubsharpClient : IDisposable
    {
        public IHttpClientFactory HttpClientFactory { get; set; }

        public StubsharpClient(string apiKey, string apiSecret, StubhubEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API key is required");
            if (string.IsNullOrWhiteSpace(apiSecret))
                throw new ArgumentException("API secret is required");

            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _environment = environment ?? StubhubEnvironment.Sandbox;

            HttpClientFactory = new HttpClientFactory();
        }

        public bool IsAuthenticated()
        {
            return _authentication != null;
        }

        public void SetAuthenticationInformation(string accessToken, string refreshToken)
        {
            _authentication = new StubsharpAuthentication
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<StubsharpAuthentication> Login(LoginRequest loginRequest)
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

            var response = await client.SendAsync(request).ConfigureAwait(false);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                dynamic error = JObject.Parse(responseBody);
                throw new Exception($"Authentication failed: {error.error_description}");
            }

            var authResponse = JsonConvert.DeserializeObject<StubsharpAuthentication>(responseBody);

            string userId = response.Headers.GetValues("X-StubHub-User-GUID")?.FirstOrDefault();
            if(!string.IsNullOrEmpty(userId)) authResponse.UserId = new Guid(userId);

            _authentication = authResponse;

            return authResponse;
        }

        public async Task<EventSearchResponse> SearchEvents(EventSearchRequest eventSearchRequest)
        {
            return await InvokeEndpoint<EventSearchResponse>(StubhubEndpoint.EventSearchV3, eventSearchRequest);
        }

        public async Task<InventorySearchResponseV1> SearchInventory(InventorySearchRequestV1 inventorySearchRequest)
        {
            return await InvokeEndpoint<InventorySearchResponseV1>(StubhubEndpoint.InventorySearchV1, inventorySearchRequest);
        }

        private async Task<T> InvokeEndpoint<T>(StubhubEndpoint endpoint, SearchRequestBase request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));

            if (endpoint.RequiresAuthorization && !IsAuthenticated())
                throw new InvalidOperationException($"{endpoint.Name} requires authentication");

            var client = HttpClientFactory.GetAuthenticatedClient(_environment, _authentication.AccessToken);
            string uri = endpoint.Url + "?" + request.ToQueryString();

            return await GetResult<T>(uri, client);
        }

        private async Task<T> GetResult<T>(string resource, HttpClient client)
        {
            var response = await client.GetAsync(resource).ConfigureAwait(false);
            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(responseBody);
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
