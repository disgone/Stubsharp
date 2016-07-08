using System;
using System.Net;
using Stubsharp.Models.EventSearch;
using Xunit;

namespace Stubsharp.Tests.Models.EventSearch
{
    public class EventSearchRequestTests
    {
        [Fact]
        public void Name_Is_Included_In_Query()
        {
            string param = "Dragon |Fire |\"Ice\"";
            var searchRequest = new EventSearchRequest { Name = param };
            Assert.Contains("name=" + WebUtility.UrlEncode(param), searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void City_Is_Included_In_Query()
        {
            string param = "\"Los Angeles\" |Chicago";
            var searchRequest = new EventSearchRequest { City = param };
            Assert.Contains("city=" +WebUtility.UrlEncode(param), searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void State_Is_Included_In_Query()
        {
            string param = "NY |AZ |AL";
            var searchRequest = new EventSearchRequest { State = param };
            Assert.Contains("state=" +WebUtility.UrlEncode(param), searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Country_Is_Included_In_Query()
        {
            string param = "UK |US";
            var searchRequest = new EventSearchRequest { Country = param };
            Assert.Contains("country=" +WebUtility.UrlEncode(param), searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void PostalCode_Is_Included_In_Query()
        {
            string param = "90210";
            var searchRequest = new EventSearchRequest { PostalCode = param };
            Assert.Contains("postalcode=" +WebUtility.UrlEncode(param), searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void MinimumPrice_Is_Included_In_Query()
        {
            var searchRequest = new EventSearchRequest { MinimumPrice = 10 };
            Assert.Contains("minPrice=10", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Radius_Is_Included_In_Query()
        {
            var searchRequest = new EventSearchRequest { Radius = 10.4 };
            Assert.Contains("radius=10.4", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Units_Is_Included_In_Query()
        {
            var searchRequest = new EventSearchRequest { Units = RadiusUnits.Kilometers };
            Assert.Contains("units=km", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void MinimumAvailableTickets_Is_Included_In_Query()
        {
            var searchRequest = new EventSearchRequest { MinimumAvailableTickets = 6 };
            Assert.Contains("minAvailableTickets=6", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Parking_Is_Included_In_Query()
        {
            var searchRequest = new EventSearchRequest { Parking = true };
            Assert.Contains("parking=true", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);

            searchRequest.Parking = null;
            Assert.DoesNotContain("parking", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Start_Is_Included_In_Query()
        {
            var searchRequest = new EventSearchRequest { Start = 541 };
            Assert.Contains("start=541", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Rows_Is_Included_In_Query()
        {
            var searchRequest = new EventSearchRequest { Rows = 108 };
            Assert.Contains("rows=108", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);

            searchRequest.Rows = null;
            Assert.DoesNotContain("rows", searchRequest.ToQueryString(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Rows_Does_Not_Allow_Greater_Than_500()
        {
            var searchRequest = new EventSearchRequest();
            Assert.Throws<ArgumentOutOfRangeException>(() => searchRequest.Rows = 501);
        }
    }
}
