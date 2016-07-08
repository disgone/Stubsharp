using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stubsharp;
using Stubsharp.Models.EventSearch;
using Xunit;
using Xunit.Abstractions;

namespace Stubsharp.Tests
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

    public class StubsharpClientTests : IClassFixture<ClientTestFixture>
    {
        private ITestOutputHelper Output { get; set; }
        private IConfiguration Configuration { get; set; }
        public ClientTestFixture Fixture { get; set; }

        public StubsharpClientTests(ClientTestFixture fixture, ITestOutputHelper output)
        {
            Fixture = fixture;
            Output = output;
        }

        [Fact]
        public async void Can_Login()
        {
            var response = await Fixture.Client.Login(new LoginRequest(Fixture.UserName, Fixture.Password));
        }

        [Fact]
        public async void Event_Search()
        {
            if ( !Fixture.Client.IsAuthenticated() )
            {
                Fixture.UsePreAuthorizationToken();
            }

            var request = new EventSearchRequest();
            request.Name = "\"Texas Rangers\" |Minnesota |Baltimore";
            request.Rows = 100;
            request.State = "TX";
            request.SortBy(EventSearchSortKey.EventDate, SortDirection.Descending);

            var result = await Fixture.Client.SearchEvents(request);
        }

        [Fact]
        public async void Invoking_Endpoint_Requireming_Authorization_While_Unauthorized_Throws_Exception()
        {
            Assert.False(Fixture.Client.IsAuthenticated());

            var request = new EventSearchRequest
            {
                Query = "Miami Heat"
            };

            var result = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Fixture.Client.SearchEvents(request));
        }
    }
}
