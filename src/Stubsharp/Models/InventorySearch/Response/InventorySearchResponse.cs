using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.Response
{
    public class InventorySearchResponse
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

        [JsonProperty("pricingSummary")]
        public PricingSummary PricingSummary { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("rows")]
        public int Rows { get; set; }
    }
}
