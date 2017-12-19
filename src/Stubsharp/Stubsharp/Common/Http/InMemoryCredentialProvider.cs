using System.Threading.Tasks;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    public class InMemoryCredentialProvider : ICredentialProvider
    {
        private readonly Credentials _credentials;

        public InMemoryCredentialProvider(Credentials credentials)
        {
            Guard.IsNotNull(credentials, nameof(credentials));

            _credentials = credentials;
        }

        /// <summary>
        /// Gets the credentials
        /// </summary>
        public Task<Credentials> GetCredentials()
        {
            return Task.FromResult(_credentials);
        }
    }
}
