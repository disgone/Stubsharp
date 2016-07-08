using Newtonsoft.Json;

namespace Stubsharp.Models
{
    public interface IHttpResource
    {
        [JsonProperty("webURI")]
        string WebUri { get; set; }
    }
}