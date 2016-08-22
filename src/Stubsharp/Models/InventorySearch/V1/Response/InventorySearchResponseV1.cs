using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V1.Response
{
    public class InventorySearchResponseV1
    {
        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("totalListings")]
        public int TotalListings { get; set; }

        [JsonProperty("totalTickets")]
        public int TotalTickets { get; set; }

        [JsonProperty("minQuantity")]
        public int MinQuantity { get; set; }

        [JsonProperty("maxQuantity")]
        public int MaxQuantity { get; set; }

        [JsonProperty("listing")]
        public Listing[] Listings { get; set; }

        [JsonProperty("section_stats")]
        public SectionStatistic[] SectionStatistics { get; set; }

        [JsonProperty("zone_stats")]
        public ZoneStatistic[] ZoneStatistics { get; set; }

        [JsonProperty("pricingSummary")]
        public V2.Response.PricingSummary PricingSummary { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("rows")]
        public int Rows { get; set; }
    }
}
