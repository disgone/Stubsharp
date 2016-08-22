using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V1.Response
{
    public class PricingSummary : Pricing
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("medianTicketPrice")]
        public float? MedianTicketPrice { get; set; }
    }
}