using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V1.Response
{
    public abstract class Statistic : Pricing
    {
        [JsonProperty("minTicketQuantity")]
        public int MinTicketQuantity { get; set; }

        [JsonProperty("maxTicketQuantity")]
        public int MaxTicketQuantity { get; set; }

        [JsonProperty("totalTickets")]
        public int TotalTickets { get; set; }
    }
}