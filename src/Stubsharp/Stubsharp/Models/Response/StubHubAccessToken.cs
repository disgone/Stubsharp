using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Stubsharp.Common.Http;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Models.Response
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class StubHubAccessToken
    {
        private DateTimeOffset? _expirationDate;
        private DateTimeOffset _requestDate;

        public StubHubAccessToken()
        {
            _requestDate = DateTimeOffset.UtcNow;
        }

        public StubHubAccessToken(string accessToken, string refreshToken)
            : this()
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>The access token.</value>
        [JsonProperty("access_token")]
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        /// <value>The refresh token.</value>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; private set; }

        /// <summary>
        /// Gets the token lifetime in seconds
        /// </summary>
        /// <value>The expire time.</value>
        [JsonProperty("expires_in")]
        public int ExpireTime { get; private set; }

        /// <summary>
        /// Gets the estimated token expiration date.
        /// </summary>
        /// <remarks>
        /// This estimates the expiration date based on the original request date.
        /// </remarks>
        /// <value>The token expiration date.</value>
        public DateTimeOffset TokenExpirationDate
        {
            get
            {
                if ( !_expirationDate.HasValue )
                {
                    _expirationDate = _requestDate.Add(ExpireTime.Seconds());
                }

                return _expirationDate.GetValueOrDefault();
            }
            set => _expirationDate = value;
        }

        /// <summary>
        /// Determines whether this token instance is expired.
        /// </summary>
        /// <returns><c>true</c> if this instance is expired; otherwise, <c>false</c>.</returns>
        public bool IsExpired()
        {
            return _expirationDate <= DateTimeOffset.Now;
        }

        /// <summary>
        /// Returns the amount of time remaining until the token expires.
        /// </summary>
        /// <returns>TimeSpan.</returns>
        public TimeSpan TimeRemaining()
        {
            return IsExpired() ? TimeSpan.Zero : DateTimeOffset.Now.Subtract(TokenExpirationDate);
        }

        /// <summary>
        /// Composes the <see cref="StubHubAccessToken"/> to <see cref="Credentials"/>
        /// </summary>
        public Credentials AsCredentials()
        {
            return new Credentials(UserId, AccessToken, RefreshToken)
            {
                ExpirationDate = TokenExpirationDate
            };
        }

        internal string DebuggerDisplay => $"UserId={UserId}; AccessToken={AccessToken}; RefreshToken={RefreshToken}";
    }
}
