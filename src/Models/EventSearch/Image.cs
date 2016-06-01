using Newtonsoft.Json;

namespace Stubsharp.Models.EventSearch
{
    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("isResizable")]
        public bool IsResizable { get; set; }

        [JsonProperty("urlSsl")]
        public string UrlSsl { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("credit")]
        public string Credit { get; set; }
    }
}