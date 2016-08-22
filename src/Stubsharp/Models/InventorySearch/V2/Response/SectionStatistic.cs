using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V2.Response
{
    public class SectionStatistic
    {
        [JsonProperty("sectionId")]
        public int SectionId { get; set; }

        [JsonProperty("sectionName")]
        public string SectionName { get; set; }

        [JsonProperty("minTicketPrice")]
        public float MinTicketPrice { get; set; }

        [JsonProperty("maxTicketPrice")]
        public float MaxTicketPrice { get; set; }

        [JsonProperty("averageTicketPrice")]
        public float? AverageTicketPrice { get; set; }

        [JsonProperty("minTicketQuantity")]
        public int MinTicketQuantity { get; set; }

        [JsonProperty("maxTicketQuantity")]
        public int MaxTicketQuantity { get; set; }

        [JsonProperty("totalTickets")]
        public int TotalTickets { get; set; }

        [JsonProperty("totalListings")]
        public int TotalListings { get; set; }
    }
}