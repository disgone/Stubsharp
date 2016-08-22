using Newtonsoft.Json;

namespace Stubsharp.Models.Common.Response
{
    public abstract class NamedItem : IIdentifier<int>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
