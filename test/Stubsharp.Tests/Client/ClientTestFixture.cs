using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Stubsharp.Tests.Client
{
    public class ClientTestFixture : IDisposable
    {
        public StubsharpClient Client { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }

        public ClientTestFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var key = config["Stubhub:Key"];
            var secret = config["Stubhub:Secret"];
            var environment = config["Stubhub:Environment"];

            var env = string.Equals("Sandbox", environment, StringComparison.OrdinalIgnoreCase)
                ? StubhubEnvironment.Sandbox
                : StubhubEnvironment.Production;

            UserName = config["Stubhub:UserName"];
            Password = config["Stubhub:Password"];
            AuthToken = config["Stubhub:AuthToken"];

            Client = new StubsharpClient(key, secret, env);
        }

        public void UsePreAuthorizationToken()
        {
            if (string.IsNullOrWhiteSpace(AuthToken))
                throw new InvalidOperationException("No auth token found in appsettings");

            Client.SetAuthenticationInformation(AuthToken, null);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}