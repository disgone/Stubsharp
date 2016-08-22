using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V2.Response
{
    public class PricingSummary
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("minTicketPrice")]
        public float? MinTicketPrice { get; set; }

        [JsonProperty("averageTicketPrice")]
        public float? AverageTicketPrice { get; set; }

        [JsonProperty("maxTicketPrice")]
        public float? MaxTicketPrice { get; set; }

        [JsonProperty("totalListings")]
        public int TotalListings { get; set; }
    }
}