using System;
using Newtonsoft.Json;
using Stubsharp.Models.Common.Request;
using Stubsharp.Models.InventorySearch.Request;
using Xunit;
using Xunit.Abstractions;

namespace Stubsharp.Tests.Client
{
    public class StubsharpClientInventorySearchTests : IDisposable
    {
        private ITestOutputHelper Output { get; set; }
        public ClientTestFixture Fixture { get; set; }

        public StubsharpClientInventorySearchTests(ITestOutputHelper output)
        {
            Fixture = new ClientTestFixture();
            Output = output;
        }

        [Fact]
        public async void Inventory_Search()
        {
            if (!Fixture.Client.IsAuthenticated())
            {
                Fixture.UsePreAuthorizationToken();
            }

            var request = new InventorySearchRequest(9442860)
            {
                Rows = 5000,
            };
            request.SortBy(InventorySearchSortKey.CurrentPrice, SortDirection.Descending);

            var result = await Fixture.Client.SearchInventory(request);

            Output.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Fact]
        public async void Inventory_Search_Requires_Authorization()
        {
            Assert.False(Fixture.Client.IsAuthenticated());

            var request = new InventorySearchRequest(9442860);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Fixture.Client.SearchInventory(request));
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}