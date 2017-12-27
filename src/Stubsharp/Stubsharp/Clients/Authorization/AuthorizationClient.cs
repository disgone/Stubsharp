using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Stubsharp.Common.Http;
using Stubsharp.Models.Response;

namespace Stubsharp.Clients.Authorization
{
    public class AuthorizationClient : ApiClient, IAuthorizationClient
    {
        public AuthorizationClient(IConnectionManager connectionManager)
            : base(connectionManager)
        {
        }

        /// <summary>
        /// Requests an access token for the user name and password combination
        /// </summary>
        /// <param name="userName">The userName</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task&lt;Stubsharp.Models.Response.StubHubAccessToken&gt;.</returns>
        public async Task<StubHubAccessToken> GetAccessToken(string userName, string password,
            CancellationToken cancellationToken = default)
        {
            var authentication = new LoginAuthenticationProvider(Connection.ClientSettings.ConsumerKey,
                Connection.ClientSettings.ConsumerSecret);

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["username"] = userName,
                ["password"] = password
            });

            var reply = await Connection.Post<StubHubAccessToken>(Endpoints.AccessToken, content, null, authentication,
                cancellationToken);

            reply.Body.UserId = reply.HttpResponse.Metadata.UserId;

            return reply.Body;
        }

        /// <summary>
        /// Renews the access token
        /// </summary>
        /// <param name="accessToken">The stubhub access token to renew</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task&lt;Stubsharp.Models.Response.StubHubAccessToken&gt;.</returns>
        public Task<StubHubAccessToken> RenewAccessToken(StubHubAccessToken accessToken,
            CancellationToken cancellationToken = default)
        {
            return RenewTokenInternal(accessToken.RefreshToken, accessToken.UserId, cancellationToken);
        }

        /// <summary>
        /// Renews the access token
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task&lt;Stubsharp.Models.Response.StubHubAccessToken&gt;.</returns>
        public Task<StubHubAccessToken> RenewAccessToken(Credentials credentials,
            CancellationToken cancellationToken = default)
        {
            return RenewTokenInternal(credentials.RefreshToken, credentials.UserId, cancellationToken);
        }

        private async Task<StubHubAccessToken> RenewTokenInternal(string refreshToken, string userId,
            CancellationToken cancellationToken = default)
        {
            var authentication = new LoginAuthenticationProvider(Connection.ClientSettings.ConsumerKey,
                Connection.ClientSettings.ConsumerSecret);

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken
            });

            var reply = await Connection.Post<StubHubAccessToken>(Endpoints.AccessToken, content, null, authentication,
                cancellationToken);

            reply.Body.UserId = userId;

            return reply.Body;
        }
    }
}
