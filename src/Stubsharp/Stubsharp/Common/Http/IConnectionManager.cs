using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stubsharp.Common.Http
{
    public interface IConnectionManager
    {
        /// <summary>
        /// Gets or sets the credentials used for the connection.
        /// </summary>
        /// <value>The credentials.</value>
        Credentials Credentials { get; set; }

        /// <summary>
        /// Gets the credential provider.
        /// </summary>
        /// <value>The credential provider.</value>
        ICredentialProvider CredentialProvider { get; }

        /// <summary>
        /// Gets the environment the connection is configured to use.
        /// </summary>
        /// <value>The environment.</value>
        StubHubEnvironment Environment { get; }

        /// <summary>
        /// Gets the client settings.
        /// </summary>
        /// <value>The client settings.</value>
        ClientSettings ClientSettings { get; }

        /// <summary>
        /// Issues a HTTP GET request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IApiResponse<T>> Get<T>(Uri uri, CancellationToken cancellationToken = default);

        /// <summary>
        /// Issues a HTTP GET request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <param name="accepts">The accepted media type</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, CancellationToken cancellationToken = default);

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IApiResponse<T>> Post<T>(Uri uri, CancellationToken cancellationToken = default);

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="body">The object to be sent as the body of the request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IApiResponse<T>> Post<T>(Uri uri, object body, CancellationToken cancellationToken = default);

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="body">The object to be sent as the body of the request</param>
        /// <param name="accepts">The accepted media type</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, CancellationToken cancellationToken = default);

        /// <summary>
        /// Issues a HTTP POST request to the provided uri.
        /// </summary>
        /// <typeparam name="T">The expected response type</typeparam>
        /// <param name="uri">The URI of the endpoint to issue the request.</param>
        /// <param name="body">The object to be sent as the body of the request</param>
        /// <param name="accepts">The accepted media type</param>
        /// <param name="authentication">The authentication to use for the request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, IAuthenticationProvider authentication, CancellationToken cancellationToken = default);
    }
}
