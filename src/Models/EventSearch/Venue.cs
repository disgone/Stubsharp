using Newtonsoft.Json;
using Stubsharp.Models.Base;

namespace Stubsharp.Models.EventSearch
{
    public class Venue : IHttpResource
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("webURI")]
        public string WebUri { get; set; }

        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("address1")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}