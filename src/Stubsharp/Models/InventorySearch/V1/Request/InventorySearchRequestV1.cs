using System;
using Stubsharp.Models.Common.Request;
using Stubsharp.Models.EventSearch.Request;
using Stubsharp.Utility;

namespace Stubsharp.Models.InventorySearch.V1.Request
{
    public class InventorySearchRequestV1 : SearchRequestBase
    {
        public InventorySearchRequestV1(long eventId)
        {
            if (eventId <= 0)
                throw new Exception("Invalid Event id");

            EventId = eventId;
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
        public double? MinimumPrice
        {
            get { return _priceMin; }
            set
            {
                _priceMin = value;
                Params.SetOrRemove("priceMin", _priceMin.ToString());
            }
        }

        /// <summary>
        /// The maximum ticket price. Only tickets less than this price will be included in the
        /// response.
        /// </summary>
        /// <value>The maximum price.</value>
        public double? MaximumPrice
        {
            get { return _priceMax; }
            set
            {
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

        public SearchRequestBase SortBy(InventorySearchSortKeyV1 key, SortDirection direction)
        {
            return base.SortBy(key, direction);
        }

        private long _eventId;
        private uint? _rows;
        private uint? _start;
        private uint? _quantity;
        private double? _priceMin;
        private double? _priceMax;
        private bool? _sectionStats;
        private bool? _zoneStats;
        private bool? _pricingSummary;
    }

    public sealed class InventorySearchSortKeyV1 : ISearchSortKey
    {
        public static InventorySearchSortKeyV1 CurrentPrice = new InventorySearchSortKeyV1("currentPrice");
        public static InventorySearchSortKeyV1 Quantity = new InventorySearchSortKeyV1("quantity");
        public static InventorySearchSortKeyV1 Row = new InventorySearchSortKeyV1("row");
        public static InventorySearchSortKeyV1 SectionName = new InventorySearchSortKeyV1("sectionName");

        public InventorySearchSortKeyV1(string key)
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
