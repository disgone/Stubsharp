using Newtonsoft.Json;

namespace Stubsharp.Models.EventSearch
{
    public class TicketInfo
    {
        [JsonProperty("minPrice")]
        public decimal MinPrice { get; set; }

        [JsonProperty("maxPrice")]
        public decimal MaxPrice { get; set; }

        [JsonProperty("totalTickets")]
        public int TotalTickets { get; set; }

        [JsonProperty("totalListings")]
        public int TotalListings { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
    }
}