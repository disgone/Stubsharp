using Stubsharp.Clients.Authorization;
using Stubsharp.Common.Http;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp
{
    public class StubsharpClient
    {
        public StubsharpClient(ClientSettings package)
            : this(new ConnectionManager(package, StubHubEnvironment.Production))
        {
        }

        public StubsharpClient(
            ClientSettings package, 
            StubHubEnvironment environment)
            : this(new ConnectionManager(package, environment))
        {
        }

        public StubsharpClient(
            ClientSettings package, 
            StubHubEnvironment environment,
            ICredentialProvider credentials)
            : this(new ConnectionManager(package, environment, credentials))
        {
        }

        public StubsharpClient(IConnectionManager connectionManager)
        {
            Guard.IsNotNull(connectionManager, nameof(connectionManager));

            Connection = connectionManager;

            Authorization = new AuthorizationClient(Connection);
        }

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public IConnectionManager Connection { get; }

        public Credentials Credentials
        {
            get => Connection.Credentials;
            set
            {
                Guard.IsNotNull(value, nameof(value));
                Connection.Credentials = value;
            }
        }

        /// <summary>
        /// Gets the StubHub environment.
        /// </summary>
        /// <value>The environment.</value>
        public StubHubEnvironment Environment => Connection.Environment;

        public IAuthorizationClient Authorization { get; private set; }
    }
}
