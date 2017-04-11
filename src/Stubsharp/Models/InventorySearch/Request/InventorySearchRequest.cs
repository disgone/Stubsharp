using System;
using Stubsharp.Models.Common.Request;
using Stubsharp.Utility;

namespace Stubsharp.Models.InventorySearch.Request
{
    public class InventorySearchRequest : SearchRequestBase
    {
        public InventorySearchRequest(long eventId)
        {
            if ( eventId <= 0 )
            {
                throw new ArgumentException("Invalid event id", nameof(eventId));
            }

            EventId = eventId;
            Start = 0;
            Rows = 100;
        }

        /// <summary>
        /// The StubHub ID of the event to get listings for.
        /// </summary>
        /// <value>The event identifier.</value>
        public long EventId
        {
            get
            {
                return _eventId;
            }
            protected set
            {
                _eventId = value;
                Params.SetOrRemove("eventId", _eventId.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the start index of the listings to return.
        /// </summary>
        /// <value>The start index.</value>
        public uint? Start
        {
            get { return _start; }
            set
            {
                _start = value;
                Params.SetOrRemove("start", _start.ToString());
            }
        }

        /// <summary>
        /// The number of rows to include in the response. The default is 50.
        /// </summary>
        /// <value>The rows.</value>
        public uint? Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                Params.SetOrRemove("rows", _rows.ToString());
            }
        }

        /// <summary>
        /// If the quantity is specified, only listings which can be bought in the quantity will be
        /// included in the response.
        /// </summary>
        /// <value>The ticket quantity required.</value>
        public uint? Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                Params.SetOrRemove("quantity", _quantity.ToString());
            }
        }


        /// <summary>
        /// The minimum ticket price. Only tickets greater than this price will be included in the
        /// response.
        /// </summary>
        /// <value>The minimum price.</value>
        public decimal? MinimumPrice
        {
            get { return _priceMin; }
            set
            {
                if(value < 0) throw new ArgumentException($"{nameof(MinimumPrice)} must be a positive value");
                _priceMin = value;
                Params.SetOrRemove("priceMin", _priceMin.ToString());
            }
        }

        /// <summary>
        /// The maximum ticket price. Only tickets less than this price will be included in the
        /// response.
        /// </summary>
        /// <value>The maximum price.</value>
        public decimal? MaximumPrice
        {
            get { return _priceMax; }
            set
            {
                if (value < 0) throw new ArgumentException($"{nameof(MaximumPrice)} must be a positive value");
                _priceMax = value;
                Params.SetOrRemove("priceMax", _priceMax.ToString());
            }
        }

        public bool? IncludeSectionStats
        {
            get { return _sectionStats; }
            set
            {
                _sectionStats = value;
                if (_sectionStats.HasValue)
                {
                    Params.SetOrRemove("sectionStats", _sectionStats.ToString());
                }
                else
                {
                    Params.Remove("sectionStats");
                }
            }
        }

        public bool? IncludeZoneStats
        {
            get { return _zoneStats; }
            set
            {
                _zoneStats = value;
                if (_zoneStats.HasValue)
                {
                    Params.SetOrRemove("zoneStats", _zoneStats.ToString());
                }
                else
                {
                    Params.Remove("zoneStats");
                }
            }
        }

        public bool? IncludePricingSummary
        {
            get { return _pricingSummary; }
            set
            {
                _pricingSummary = value;
                if (_pricingSummary.HasValue)
                {
                    Params.SetOrRemove("pricingSummary", _pricingSummary.ToString());
                }
                else
                {
                    Params.Remove("pricingSummary");
                }
            }
        }

        public bool? IncludeDeliveryTypeSummary
        {
            get { return _deliveryTypeSummary; }
            set
            {
                _deliveryTypeSummary = value;
                if (_deliveryTypeSummary.HasValue)
                {
                    Params.SetOrRemove("deliveryTypeSummary", _deliveryTypeSummary.ToString());
                }
                else
                {
                    Params.Remove("deliveryTypeSummary");
                }
            }
        }

        public bool? IncludeQuantitySummary
        {
            get { return _quantitySummary; }
            set
            {
                _quantitySummary = value;
                if (_quantitySummary.HasValue)
                {
                    Params.SetOrRemove("quantitySummary", _quantitySummary.ToString());
                }
                else
                {
                    Params.Remove("quantitySummary");
                }
            }
        }

        public bool? IncludeAllSectionZoneStats
        {
            get { return _allSectionZoneStats; }
            set
            {
                _allSectionZoneStats = value;
                if (_allSectionZoneStats.HasValue)
                {
                    Params.SetOrRemove("allSectionZoneStats", _allSectionZoneStats.ToString());
                }
                else
                {
                    Params.Remove("_allSectionZoneStats");
                }
            }
        }

        public SearchRequestBase SortBy(InventorySearchSortKey key, SortDirection direction)
        {
            return base.SortBy(key, direction);
        }

        private long _eventId;
        private uint? _rows;
        private uint? _start;
        private uint? _quantity;
        private decimal? _priceMin;
        private decimal? _priceMax;
        private bool? _sectionStats;
        private bool? _zoneStats;
        private bool? _pricingSummary;
        private bool? _deliveryTypeSummary;
        private bool? _quantitySummary;
        private bool? _allSectionZoneStats;
    }

    public sealed class InventorySearchSortKey : ISearchSortKey
    {
        public static InventorySearchSortKey CurrentPrice = new InventorySearchSortKey("currentPrice");
        public static InventorySearchSortKey ListingPrice = new InventorySearchSortKey("listingPrice");
        public static InventorySearchSortKey Quantity = new InventorySearchSortKey("quantity");
        public static InventorySearchSortKey Row = new InventorySearchSortKey("row");
        public static InventorySearchSortKey SectionName = new InventorySearchSortKey("sectionName");
        public static InventorySearchSortKey Value = new InventorySearchSortKey("value");

        public InventorySearchSortKey(string key)
        {
            _key = key;
        }

        public string SortKey => _key;

        public override string ToString()
        {
            return _key;
        }

        private string _key { get; set; }
    }
}
