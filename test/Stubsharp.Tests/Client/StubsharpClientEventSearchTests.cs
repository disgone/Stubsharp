using System;
using Newtonsoft.Json;
using Stubsharp.Models.Common.Request;
using Stubsharp.Models.EventSearch.Request;
using Xunit;
using Xunit.Abstractions;

namespace Stubsharp.Tests.Client
{
    public class StubsharpClientEventSearchTests : IDisposable
    {
        private ITestOutputHelper Output { get; set; }
        public ClientTestFixture Fixture { get; set; }

        public StubsharpClientEventSearchTests(ITestOutputHelper output)
        {
            Fixture = new ClientTestFixture();
            Output = output;
        }

        [Fact]
        public async void Event_Search()
        {
            if (!Fixture.Client.IsAuthenticated())
            {
                Fixture.UsePreAuthorizationToken();
            }

            var request = new EventSearchRequest
            {
                Name = "Rangers",
                Rows = 500,
                City = "Arlington",
                Parking = false
            };
            request.SortBy(EventSearchSortKey.EventDate, SortDirection.Descending);

            var result = await Fixture.Client.SearchEvents(request);

            Output.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Fact]
        public async void Event_Search_Requires_Authorization()
        {
            Assert.False(Fixture.Client.IsAuthenticated());

            var request = new EventSearchRequest() {Name = "Rangers"};

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await Fixture.Client.SearchEvents(request));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}
