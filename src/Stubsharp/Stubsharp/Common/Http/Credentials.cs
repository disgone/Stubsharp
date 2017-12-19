using System;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    public class Credentials
    {
        public static readonly Credentials Anonymous = new Credentials();

        private Credentials()
        {
            AuthenticationType = AuthenticationType.Anonymous;
        }

        /// <summary>
        /// StubHub access token credentials
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        public Credentials(string userId, string accessToken, string refreshToken)
        {
            Guard.IsNotNullOrEmpty(userId, nameof(userId));
            Guard.IsNotNullOrEmpty(accessToken, nameof(accessToken));

            UserId = userId;
            AccessToken = accessToken;
            RefreshToken = refreshToken;

            AuthenticationType = AuthenticationType.Bearer;
        }

        internal Credentials(string consumerKey, string consumerSecret)
        {
            Guard.IsNotNullOrEmpty(consumerKey, nameof(consumerKey));
            Guard.IsNotNullOrEmpty(consumerSecret, nameof(consumerSecret));

            UserId = consumerKey;
            AccessToken = consumerSecret;

            AuthenticationType = AuthenticationType.Basic;
        }

        public string UserId { get; }
        public string AccessToken { get; }
        public string RefreshToken { get; }
        public AuthenticationType AuthenticationType { get; }
        public DateTimeOffset ExpirationDate { get; set; } = DateTimeOffset.MaxValue;
    }

    public enum AuthenticationType
    {
        Anonymous,
        Basic,
        Bearer
    }
}
