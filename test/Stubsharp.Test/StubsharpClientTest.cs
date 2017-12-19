using Stubsharp.Common.Http;
using Xunit;

namespace Stubsharp.Test
{
    public class StubsharpClientTest
    {
        public class Authorization
        {
            [Fact]
            public void DefaultsToAnonymousCredentials()
            {
                var client = new StubsharpClient(new ClientPackageHeader("StubsharpClientTest", "1.0"), StubHubEnvironment.Sandbox);

                Assert.Equal(AuthenticationType.Anonymous, client.Credentials.AuthenticationType);
            }

            [Fact]
            public void CreatesBearerClient()
            {
                var client = new StubsharpClient(new ClientPackageHeader("StubsharpClientTest", "1.0"),
                    StubHubEnvironment.Sandbox)
                {
                    Credentials = new Credentials("uid", "access", "refresh")
                };

                Assert.Equal(AuthenticationType.Bearer, client.Credentials.AuthenticationType);
            }
        }

        public class Environment
        {
            [Fact]
            public void UsesSelectedEnvironment()
            {
                var sandbox = new StubsharpClient(new ClientPackageHeader("StubsharpClientTest", "1.0"), StubHubEnvironment.Sandbox);
                var production = new StubsharpClient(new ClientPackageHeader("StubsharpClientTest", "1.0"), StubHubEnvironment.Production);

                Assert.Equal(StubHubEnvironment.Sandbox, sandbox.Environment);
                Assert.Equal(StubHubEnvironment.Production, production.Environment);
            }
        }

        public class Demo
        {
            [Fact]
            public void UsesSelectedEnvironment()
            {
                var sandbox = new StubsharpClient(new ClientPackageHeader("StubsharpClientTest", "1.0"), StubHubEnvironment.Sandbox);

                sandbox.
            }
        }
    }
}
