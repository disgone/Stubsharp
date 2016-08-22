using Newtonsoft.Json;

namespace Stubsharp.Models.Common.Response
{
    public interface IHttpResource
    {
        [JsonProperty("webURI")]
        string WebUri { get; set; }
    }
}