using Stubsharp.Common.Http;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp
{
    public class StubsharpClient
    {
        public StubsharpClient(ClientPackageHeader package)
            : this(new ConnectionManager(package, StubHubEnvironment.Production))
        {
        }

        public StubsharpClient(
            ClientPackageHeader package, 
            StubHubEnvironment environment)
            : this(new ConnectionManager(package, environment))
        {
        }

        public StubsharpClient(
            ClientPackageHeader package, 
            StubHubEnvironment environment,
            ICredentialProvider credentials)
            : this(new ConnectionManager(package, environment, credentials))
        {
        }

        public StubsharpClient(IConnectionManager connectionManager)
        {
            Guard.IsNotNull(connectionManager, nameof(connectionManager));

            Connection = connectionManager;
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
    }
}
