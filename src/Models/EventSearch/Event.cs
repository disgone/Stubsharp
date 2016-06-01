using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stubsharp.Models.Base;

namespace Stubsharp.Models.EventSearch
{
    public class Event : IHttpResource
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventStatus Status { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("webURI")]
        public string WebUri { get; set; }

        [JsonProperty("eventDateLocal")]
        public DateTime EventDateLocal { get; set; }

        [JsonProperty("eventDateUTC")]
        public DateTime EventDateUtc { get; set; }

        [JsonProperty("venue")]
        public Venue Venue { get; set; }

        [JsonProperty("ticketInfo")]
        public TicketInfo TicketInfo { get; set; }

        [JsonProperty("ancestors")]
        public Ancestors Ancestors { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; } 
    }
}