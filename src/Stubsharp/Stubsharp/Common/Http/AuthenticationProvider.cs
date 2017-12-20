using System.Collections.Generic;
using System.Threading.Tasks;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Applies the authentication headers to the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task.</returns>
        Task Apply(IRequest request);
    }

    internal class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly Dictionary<AuthenticationType, IAuthenticationHandler> _handlers = 
            new Dictionary<AuthenticationType, IAuthenticationHandler>
            {
                { AuthenticationType.Anonymous, new AnonymousAuthenticationHandler() },
                { AuthenticationType.Basic, new BasicAuthenticationHandler() },
                { AuthenticationType.Bearer, new BearerTokenAuthentication() }
            };

        public AuthenticationProvider(ICredentialProvider credentialProvider)
        {
            Guard.IsNotNull(credentialProvider, nameof(credentialProvider));

            CredentialProvider = credentialProvider;
        }

        public async Task Apply(IRequest request)
        {
            Guard.IsNotNull(request, nameof(request));

            var credentials = await CredentialProvider.GetCredentials().ConfigureAwait(false) ?? Credentials.Anonymous;
            _handlers[credentials.AuthenticationType].Authenticate(request, credentials);
        }

        public ICredentialProvider CredentialProvider { get; set; }
    }

    /// <summary>
    /// Shortcut provider for handling access token requests
    /// </summary>
    /// <seealso cref="Stubsharp.Common.Http.IAuthenticationProvider" />
    internal class LoginAuthenticationProvider : IAuthenticationProvider
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;

        public LoginAuthenticationProvider(string consumerKey, string consumerSecret)
        {
            Guard.IsNotNullOrEmpty(consumerKey, nameof(consumerKey));
            Guard.IsNotNullOrEmpty(consumerSecret, nameof(consumerSecret));

            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
        }

        /// <summary>
        /// Applies the authentication headers to the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task.</returns>
        public async Task Apply(IRequest request)
        {
            var credentials = new Credentials(_consumerKey, _consumerSecret);
            new BasicAuthenticationHandler().Authenticate(request, credentials);
        }
    }
}
