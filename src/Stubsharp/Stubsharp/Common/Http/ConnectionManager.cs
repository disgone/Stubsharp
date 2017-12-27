using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Stubsharp.Common.Infrastructure;
using Stubsharp.Common.Serialization;

namespace Stubsharp.Common.Http
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IHttpClient _httpClient;
        private readonly ISerializer _serializer;
        private readonly AuthenticationProvider _authentication;

        public ConnectionManager(ClientSettings package, StubHubEnvironment environment)
            : this(package, environment, new InMemoryCredentialProvider(Credentials.Anonymous), new HttpClientAdapter(DefaultHandlerFactory.Create), new JsonNetSerializer())
        {
        }

        public ConnectionManager(
            ClientSettings package,
            StubHubEnvironment environment,
            ICredentialProvider credentialProvider)
            : this(package, environment, credentialProvider, new HttpClientAdapter(DefaultHandlerFactory.Create), new JsonNetSerializer())
        {
        }

        public ConnectionManager(ClientSettings package,
            StubHubEnvironment environment,
            ICredentialProvider credentialProvider,
            IHttpClient httpClient,
            ISerializer serializer)
        {
            Guard.IsNotNull(package, nameof(package));
            Guard.IsNotNull(environment, nameof(environment));
            Guard.IsNotNull(credentialProvider, nameof(credentialProvider));
            Guard.IsNotNull(httpClient, nameof(httpClient));
            Guard.IsNotNull(serializer, nameof(serializer));

            BaseAddress = environment.BaseUri;
            ClientSettings = package;
            UserAgent = GetUserAgent(package);
            Environment = environment;

            _httpClient = httpClient;
            _authentication = new AuthenticationProvider(credentialProvider);
            _serializer = serializer;
        }

        /// <summary>
        /// Gets the base address.
        /// </summary>
        /// <value>The base address.</value>
        public Uri BaseAddress { get; private set; }

        /// <summary>
        /// The user agent string associated with the connection
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent { get; private set; }

        /// <summary>
        /// Gets the credential provider.
        /// </summary>
        /// <value>The credential provider.</value>
        public ICredentialProvider CredentialProvider => _authentication.CredentialProvider;

        /// <summary>
        /// Gets the environment the connection is configured to use.
        /// </summary>
        /// <value>The environment.</value>
        public StubHubEnvironment Environment { get; }

        /// <summary>
        /// Gets the client settings.
        /// </summary>
        /// <value>The client settings.</value>
        public ClientSettings ClientSettings { get; }

        /// <summary>
        /// The connections used for the connection
        /// </summary>
        /// <value>The credentials.</value>
        public Credentials Credentials
        {
            get
            {
                var credentialTask = CredentialProvider.GetCredentials();

                if (credentialTask == null)
                {
                    return Credentials.Anonymous;
                }

                return credentialTask.Result ?? Credentials.Anonymous;
            }
            set
            {
                Guard.IsNotNull(value, nameof(value));
                _authentication.CredentialProvider = new InMemoryCredentialProvider(value);
            }
        }


        /// <summary>
        /// Issues a HTTP GET request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public Task<IApiResponse<T>> Get<T>(Uri uri, CancellationToken cancellationToken = default)
        {
            return Get<T>(uri, null, null, cancellationToken);
        }

        /// <summary>
        /// Issues a HTTP GET request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <param name="accepts">The accepted media types</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(uri, nameof(uri));

            var uriData = uri.ApplyParameters(parameters);

            return Send<T>(uriData, HttpMethod.Get, null, accepts, _authentication, cancellationToken);
        }

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public Task<IApiResponse<T>> Post<T>(Uri uri, CancellationToken cancellationToken = default)
        {
            return Post<T>(uri, null, null, cancellationToken);
        }

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="body">The object to be sent as the body of the request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, CancellationToken cancellationToken = default)
        {
            return Post<T>(uri, body, "application/json", _authentication, cancellationToken);
        }

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="body">The object to be sent as the body of the request</param>
        /// <param name="accepts">The accepted media types</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, CancellationToken cancellationToken = default)
        {
            return Post<T>(uri, body, accepts, _authentication, cancellationToken);
        }

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <remarks>This overload exists so we can issue the request and renewal for access tokens</remarks>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="body">The object to be sent as the body of the request</param>
        /// <param name="accepts">The accepted media types</param>
        /// <param name="authentication">The authentication to use with this request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, IAuthenticationProvider authentication, CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(uri, nameof(uri));

            return Send<T>(uri, HttpMethod.Post, body, accepts, authentication, cancellationToken);
        }

        private Task<IApiResponse<T>> Send<T>(Uri uri, HttpMethod method, object body, string accepts = "application/json",
            IAuthenticationProvider authentication = null, CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(uri, nameof(uri));

            var request = new Request
            {
                Method = method,
                BaseAddress = BaseAddress,
                Endpoint = uri
            };

            if ( !accepts.HasValue() )
            {
                accepts = "application/json";
            }

            request.Headers["Accept"] = accepts;

            if ( body == null )
            {
                return Execute<T>(request, authentication, cancellationToken);
            }

            request.Body = body;
            request.ContentType = "application/x-www-form-urlencoded";

            return Execute<T>(request, authentication, cancellationToken);
        }

        private async Task<IApiResponse<T>> Execute<T>(IRequest request, IAuthenticationProvider authentication, CancellationToken cancellationToken = default)
        {
            PrepareRequestContent(request);
            var response = await Execute(request, authentication, cancellationToken).ConfigureAwait(false);
            return DeserializeResponse<T>(response);
        }

        /// <summary>
        /// Executes the specified request. All requests should ultimately be run through this endpoint.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="authentication">The authentication provider for this request</param>
        /// <param name="cancellationToken">The cancellation.</param>
        /// <returns>System.Threading.Tasks.Task&lt;Stubsharp.Common.Http.IResponse&gt;.</returns>
        private async Task<IResponse> Execute(IRequest request, IAuthenticationProvider authentication, CancellationToken cancellationToken = default)
        {
            request.Headers.Add("User-Agent", UserAgent);
            await authentication.Apply(request).ConfigureAwait(false);
            return await _httpClient.Send(request, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Prepares the content of the request.
        /// </summary>
        /// <param name="request">The request.</param>
        private void PrepareRequestContent(IRequest request)
        {
            Guard.IsNotNull(request, nameof(request));

            if (request.Method == HttpMethod.Get || request.Body == null)
            {
                return;
            }

            if (request.Body is string || request.Body is Stream || request.Body is HttpContent)
            {
                return;
            }

            request.Body = _serializer.Serialize(request.Body);
        }

        /// <summary>
        /// Deserialize the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns>Stubsharp.Common.Http.IApiResponse&lt;T&gt;.</returns>
        private IApiResponse<T> DeserializeResponse<T>(IResponse response)
        {
            Guard.IsNotNull(response, nameof(response));

            if (response.ContentType != null &&
                 response.ContentType.Equals("application/json", StringComparison.Ordinal))
            {
                var body = response.Body as string;
                var content = _serializer.Deserialize<T>(body);
                return new ApiResponse<T>(response, content);
            }

            return new ApiResponse<T>(response);
        }

        internal static string GetUserAgent(ClientSettings package)
        {
            return $"{package} ({GetPlatformInformation()}; {GetCultureInformation()}; Stubsharp {GetVersionInformation()})";
        }

        private static string _platform;
        internal static string GetPlatformInformation()
        {
            if (string.IsNullOrEmpty(_platform))
            {
                try
                {
                    _platform = string.Format(CultureInfo.InvariantCulture,
#if !HAS_ENVIRONMENT
                        "{0}; {1}",
                        RuntimeInformation.OSDescription.Trim(),
                        RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant().Trim()
#else
                        "{0} {1}; {2}",
                        Environment.OSVersion.Platform,
                        Environment.OSVersion.Version.ToString(3),
                        Environment.Is64BitOperatingSystem ? "amd64" : "x86"
#endif
                    );
                }
                catch
                {
                    _platform = "Unknown Platform";
                }
            }

            return _platform;
        }

        internal static string GetCultureInformation()
        {
            return CultureInfo.CurrentCulture.Name;
        }

        private static string _versionInformation;
        internal static string GetVersionInformation()
        {
            if (string.IsNullOrEmpty(_versionInformation))
            {
                _versionInformation = typeof(StubsharpClient)
                    .GetTypeInfo()
                    .Assembly
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;
            }

            return _versionInformation;
        }
    }
}
