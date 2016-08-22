using Newtonsoft.Json;

namespace Stubsharp.Models.InventorySearch.V1.Response
{
    public class ZoneStatistic : Statistic
    {
        [JsonProperty("zoneId")]
        public int ZoneId { get; set; }

        [JsonProperty("zoneName")]
        public string ZoneName { get; set; }
    }
}