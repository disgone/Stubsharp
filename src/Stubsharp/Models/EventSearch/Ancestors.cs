using Newtonsoft.Json;

namespace Stubsharp.Models.EventSearch
{
    public class Ancestors
    {
        [JsonProperty("categories")]
        public Category[] Categories { get; set; }

        [JsonProperty("groupings")]
        public Grouping[] Groupings { get; set; }

        [JsonProperty("performers")]
        public Performer[] Performers { get; set; }
    }
}