using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V1.Response
{
    public abstract class Pricing
    {
        [JsonProperty("minTicketPrice")]
        public float? MinTicketPrice { get; set; }

        [JsonProperty("maxTicketPrice")]
        public float? MaxTicketPrice { get; set; }

        [JsonProperty("totalListings")]
        public int TotalListings { get; set; }
    }
}