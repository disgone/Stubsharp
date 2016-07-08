using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stubsharp.Models.EventSearch;
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
            HttpContent payload = new FormUrlEncodedContent(data);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", $"{TokenBuilder.CreateAuthorizationToken(_apiKey, _apiSecret)}");

            var response = await client.PostAsync(StubhubEndpoint.Login.Url, payload);
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
            if(eventSearchRequest == null)
                throw new ArgumentNullException(nameof(eventSearchRequest));

            if (StubhubEndpoint.EventSearch.RequiresAuthorization && !IsAuthenticated())
                throw new InvalidOperationException($"{StubhubEndpoint.EventSearch.Name} requires authentication");

            var client = HttpClientFactory.GetAuthenticatedClient(_environment, _authentication.AccessToken);

            string endpoint = StubhubEndpoint.EventSearch.Url + "?" + eventSearchRequest.ToQueryString();
            var response = await client.GetAsync(endpoint);
            string responseBody = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<EventSearchResponse>(responseBody);
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
