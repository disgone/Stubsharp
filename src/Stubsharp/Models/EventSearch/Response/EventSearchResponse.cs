using Newtonsoft.Json;

namespace Stubsharp.Models.EventSearch.Response
{
    public class EventSearchResponse
    {
        [JsonProperty("numFound")]
        public int TotalResults { get; set; }

        [JsonProperty("events")]
        public Event[] Events { get; set; }
    }
}