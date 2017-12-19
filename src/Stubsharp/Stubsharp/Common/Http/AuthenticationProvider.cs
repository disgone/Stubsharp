using System.Collections.Generic;
using System.Threading.Tasks;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    internal class AuthenticationProvider
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
}
