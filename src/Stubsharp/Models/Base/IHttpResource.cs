using Newtonsoft.Json;

namespace Stubsharp.Models.Base
{
    public interface IHttpResource
    {
        [JsonProperty("webURI")]
        string WebUri { get; set; }
    }
}