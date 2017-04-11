using System;
using Stubsharp.Models.Common.Request;
using Stubsharp.Utility;

namespace Stubsharp.Models.EventSearch.Request
{
    public class EventSearchRequest : SearchRequestBase
    {

        public string Query
        {
            get
            {
                return Params["q"];
            }
            set
            {
                Params.SetOrRemove("q", value);
            }
        }

        public string Name
        {
            get
            {
                return Params["name"];
            }
            set
            {
                Params.SetOrRemove("name", value);
            }
        }

        public string City
        {
            get
            {
                return Params["city"];
            }
            set
            {
                Params.SetOrRemove("city", value);
            }
        }

        public string State
        {
            get
            {
                return Params["state"];
            }
            set
            {
                Params.SetOrRemove("state", value);
            }
        }

        public string Country
        {
            get
            {
                return Params["country"];
            }
            set
            {
                Params.SetOrRemove("country", value);
            }
        }

        public string PostalCode
        {
            get
            {
                return Params["postalcode"];
            }
            set
            {
                Params.SetOrRemove("postalcode", value);
            }
        }

        public double? Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                Params.SetOrRemove("radius", _radius.ToString());
            }
        }

        public RadiusUnits Units
        {
            get
            {
                return _units;
            }
            set
            {
                _units = value;
                switch (_units)
                {
                    case RadiusUnits.Kilometers:
                        Params.SetOrRemove("units", "km");
                        break;
                    case RadiusUnits.Miles:
                        Params.SetOrRemove("units", "mi");
                        break;
                    default:
                        Params.Remove("units");
                        break;
                }
            }
        }

        public uint? MinimumAvailableTickets
        {
            get { return _minTickets; }
            set
            {
                _minTickets = value;
                Params.SetOrRemove("minAvailableTickets", _minTickets.ToString());
            }
        }

        public uint? MinimumPrice
        {
            get { return _minPrice; }
            set
            {
                _minPrice = value;
                Params.SetOrRemove("minPrice", _minPrice.ToString());
            }
        }

        public bool? Parking
        {
            get { return _parking; }
            set
            {
                _parking = value;
                if (_parking.HasValue)
                {
                    Params.SetOrRemove("parking", _parking.ToString());
                }
                else
                {
                    Params.Remove("parking");
                }
            }
        }

        public uint Start
        {
            get { return _start; }
            set
            {
                _start = value;
                Params.SetOrRemove("start", _start.ToString());
            }
        }

        public uint? Rows
        {
            get { return _rows; }
            set
            {
                if (value > 500)
                    throw new ArgumentOutOfRangeException(nameof(Rows), "Maximum row count is 500");
                _rows = value;
                Params.SetOrRemove("rows", _rows.ToString());
            }
        }

        public SearchRequestBase SortBy(EventSearchSortKey key, SortDirection direction)
        {
            return base.SortBy(key, direction);
        }

        private double? _radius;
        private uint? _minTickets;
        private uint? _minPrice;
        private bool? _parking;
        private uint _start;
        private uint? _rows;
        private RadiusUnits _units;
    }

    public enum RadiusUnits
    {
        Miles,
        Kilometers
    }

    public sealed class EventSearchSortKey : ISearchSortKey
    {
        public static EventSearchSortKey Popularity = new EventSearchSortKey("popularity");
        public static EventSearchSortKey EventDate = new EventSearchSortKey("eventDateLocal");
        public static EventSearchSortKey Distance = new EventSearchSortKey("distance");

        public EventSearchSortKey(string key)
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
