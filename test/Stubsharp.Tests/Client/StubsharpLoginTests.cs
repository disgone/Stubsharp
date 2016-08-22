using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Stubsharp.Tests.Client
{
    public class StubsharpLoginTests : IClassFixture<ClientTestFixture>
    {
        private ITestOutputHelper Output { get; set; }
        private IConfiguration Configuration { get; set; }
        public ClientTestFixture Fixture { get; set; }

        public StubsharpLoginTests(ClientTestFixture fixture, ITestOutputHelper output)
        {
            Fixture = fixture;
            Output = output;
        }

        [Fact]
        public async void Can_Login()
        {
            var response = await Fixture.Client.Login(new LoginRequest(Fixture.UserName, Fixture.Password));
            File.WriteAllText(@"F:\Temp\login.txt", JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}