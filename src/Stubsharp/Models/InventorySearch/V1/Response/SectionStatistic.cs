using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V1.Response
{
    public class SectionStatistic : Statistic
    {
        [JsonProperty("sectionId")]
        public int SectionId { get; set; }

        [JsonProperty("sectionName")]
        public string SectionName { get; set; }

        [JsonProperty("medianTicketPrice")]
        public float? MedianTicketPrice { get; set; }
    }
}