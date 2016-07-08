using Newtonsoft.Json;

namespace Stubsharp.Models
{
    public abstract class NamedItem : IIdentifier<int>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
