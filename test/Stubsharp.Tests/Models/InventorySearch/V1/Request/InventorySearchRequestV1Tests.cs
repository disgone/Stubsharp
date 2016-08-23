using System;
using System.Net;
using Stubsharp.Models.InventorySearch.V1.Request;
using Xunit;

namespace Stubsharp.Tests.Models.InventorySearch.V1.Request
{
    public class InventorySearchRequestV1Tests
    {
        [Fact]
        public void Zero_Or_Negative_Event_Id_Throws_Exception()
        {
            Assert.Throws<ArgumentException>(() => new InventorySearchRequestV1(0));
            Assert.Throws<ArgumentException>(() => new InventorySearchRequestV1(-1));
        }

        [Fact]
        public void Event_Id_Is_Included_In_QueryString()
        {
            long eventId = 12345;
            var request = new InventorySearchRequestV1(eventId);

            string queryString = request.ToQueryString();
            Assert.Contains("eventid=" + WebUtility.UrlEncode(eventId.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Start_Is_Included_In_QueryString()
        {
            uint start = 25;

            var request = new InventorySearchRequestV1(125)
            {
                Start = start
            };

            string queryString = request.ToQueryString();
            Assert.Contains("start=" + WebUtility.UrlEncode(start.ToString()), queryString, StringComparison.OrdinalIgnoreCase);

            start = 0;
            request = new InventorySearchRequestV1(125)
            {
                Start = start
            };

            queryString = request.ToQueryString();
            Assert.Contains("start=" + WebUtility.UrlEncode(start.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Start_Not_Included_In_QueryString_If_Null()
        {
            var request = new InventorySearchRequestV1(125)
            {
                Start = null
            };

            string queryString = request.ToQueryString();
            Assert.DoesNotContain("start=", queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Rows_Is_Included_In_QueryString()
        {
            uint rows = 250;

            var request = new InventorySearchRequestV1(125)
            {
                Rows = rows
            };

            string queryString = request.ToQueryString();
            Assert.Contains("rows=" + WebUtility.UrlEncode(rows.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Rows_Is_Not_Included_In_QueryString_If_Null()
        {
            var request = new InventorySearchRequestV1(125)
            {
                Rows = null
            };

            string queryString = request.ToQueryString();
            Assert.DoesNotContain("rows=", queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Quantity_Is_Included_In_QueryString()
        {
            uint quantity = 2;

            var request = new InventorySearchRequestV1(125)
            {
                Quantity = quantity
            };

            string queryString = request.ToQueryString();
            Assert.Contains("quantity=" + WebUtility.UrlEncode(quantity.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void MinimumPrice_Is_Included_In_QueryString()
        {
            decimal price = 9.25m;

            var request = new InventorySearchRequestV1(125)
            {
                MinimumPrice = price
            };

            string queryString = request.ToQueryString();
            Assert.Contains("priceMin=" + WebUtility.UrlEncode(price.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void MinimumPrice_Throws_Exception_If_Given_Negative_Amount()
        {
            Assert.Throws<ArgumentException>(() => new InventorySearchRequestV1(125) { MinimumPrice = -1.00m });
        }

        [Fact]
        public void MaximumPrice_Is_Included_In_QueryString()
        {
            decimal price = 9.25m;

            var request = new InventorySearchRequestV1(125)
            {
                MaximumPrice = price
            };

            string queryString = request.ToQueryString();
            Assert.Contains("priceMax=" + WebUtility.UrlEncode(price.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void MaximumPrice_Throws_Exception_If_Given_Negative_Amount()
        {
            Assert.Throws<ArgumentException>(() => new InventorySearchRequestV1(125) { MaximumPrice = -1.00m });
        }

        [Fact]
        public void IncludeSectionStats_Is_Included_In_QueryString()
        {
            var request = new InventorySearchRequestV1(125)
            {
                IncludeSectionStats = true
            };

            string queryString = request.ToQueryString();
            Assert.Contains("sectionStats=" + WebUtility.UrlEncode(true.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void IncludeZoneStats_Is_Included_In_QueryString()
        {
            var request = new InventorySearchRequestV1(125)
            {
                IncludeZoneStats = true
            };

            string queryString = request.ToQueryString();
            Assert.Contains("zoneStats=" + WebUtility.UrlEncode(true.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void IncludePricingSummary_Is_Included_In_QueryString()
        {
            var request = new InventorySearchRequestV1(125)
            {
                IncludePricingSummary = true
            };

            string queryString = request.ToQueryString();
            Assert.Contains("pricingSummary=" + WebUtility.UrlEncode(true.ToString()), queryString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
