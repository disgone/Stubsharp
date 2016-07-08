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

            Client = new StubsharpClient(key, secret, env);
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
        public async void Event_Search()
        {
            if ( !Fixture.Client.IsAuthenticated() )
            {
                await Fixture.Client.Login(new LoginRequest("vuyego@divismail.ru", "fVmicf7nuDkg"));
            }

            var request = new EventSearchRequest();
            request.Name = "\"Texas Rangers\" |Minnesota |Baltimore";
            request.Rows = 100;
            request.State = "TX";
            request.SortBy(EventSearchSortKey.EventDate, SortDirection.Descending);

            var result = await Fixture.Client.SearchEvents(request);
        }
    }
}
