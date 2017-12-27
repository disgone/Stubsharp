using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Stubsharp.Common.Http;
using Stubsharp.Models.Response;

namespace Stubsharp.Clients.Authorization
{
    public interface IAuthorizationClient
    {
        /// <summary>
        /// Requests a user access token
        /// </summary>
        /// <param name="userName">The userName</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<StubHubAccessToken> GetAccessToken(string userName, string password,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Renews the access token associated with the credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<StubHubAccessToken> RenewAccessToken(Credentials credentials,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Renews the access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task&lt;Stubsharp.Models.Response.StubHubAccessToken&gt;.</returns>
        Task<StubHubAccessToken> RenewAccessToken(StubHubAccessToken accessToken,
            CancellationToken cancellationToken = default);
    }
}
