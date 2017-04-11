using Newtonsoft.Json;
using Stubsharp.Models.Common.Response;
using Stubsharp.Utility;
using Stubsharp.Utility.Serializers;

namespace Stubsharp.Models.InventorySearch.V2.Response
{
    public class Listing
    {
        [JsonProperty("listingId")]
        public int ListingId { get; set; }

        [JsonProperty("currentPrice")]
        public Currency CurrentPrice { get; set; }

        [JsonProperty("listingPrice")]
        public Currency ListingPrice { get; set; }

        [JsonProperty("sectionId")]
        public int? SectionId { get; set; }

        [JsonProperty("row")]
        public string Row { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("sellerSectionName")]
        public string SellerSectionName { get; set; }

        [JsonProperty("seatNumbers")]
        public string SeatNumbers { get; set; }

        [JsonProperty("dirtyTicketInd")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool DirtyTicketInd { get; set; }

        [JsonProperty("sectionName")]
        public string SectionName { get; set; }

        [JsonProperty("zoneId")]
        public int? ZoneId { get; set; }

        [JsonProperty("zoneName")]
        public string ZoneName { get; set; }

        [JsonProperty("splitOption")]
        public string SplitOption { get; set; }

        [JsonProperty("ticketSplit")]
        public string TicketSplit { get; set; }

        [JsonProperty("splitVector")]
        public int[] SplitVector { get; set; }

        [JsonProperty("sellerOwnInd")]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool SellerOwnInd { get; set; }

        [JsonProperty("listingAttributeList")]
        public object ListingAttributeList { get; set; }

        [JsonProperty("listingAttributeCategoryList")]
        public object ListingAttributeCategoryList { get; set; }

        [JsonProperty("faceValue")]
        public Currency FaceValue { get; set; }

        [JsonProperty("deliveryTypeList")]
        public int[] DeliveryTypeList { get; set; }
    }
}