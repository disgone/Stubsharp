using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace Stubsharp.Models.EventSearch
{
    public class EventSearchRequest
    {
        public EventSearchRequest()
        {
            _params = new NameValueCollection();
            _sort = new List<Tuple<EventSearchSortKey, SortDirection>>();
        }

        public string Query
        {
            get
            {
                return _params.Get("q");
            }
            set
            {
                _params.SetOrRemove("q", value);
            }
        }

        public string Name
        {
            get
            {
                return _params["name"];
            }
            set
            {
                _params.SetOrRemove("name", value);
            }
        }

        public string City
        {
            get
            {
                return _params.Get("city");
            }
            set
            {
                _params.SetOrRemove("city", value);
            }
        }

        public string State
        {
            get
            {
                return _params.Get("state");
            }
            set
            {
                _params.SetOrRemove("state", value);
            }
        }

        public string Country
        {
            get
            {
                return _params.Get("country");
            }
            set
            {
                _params.SetOrRemove("country", value);
            }
        }

        public string PostalCode
        {
            get
            {
                return _params.Get("postalcode");
            }
            set
            {
                _params.SetOrRemove("postalcode", value);
            }
        }

        public double? Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                _params.SetOrRemove("radius", _radius.ToString());
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
                        _params.SetOrRemove("units", "km");
                        break;
                    case RadiusUnits.Miles:
                        _params.SetOrRemove("units", "mi");
                        break;
                    default:
                        _params.Remove("units");
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
                _params.SetOrRemove("minAvailableTickets", _minTickets.ToString());
            }
        }

        public uint? MinimumPrice
        {
            get { return _minPrice; }
            set
            {
                _minPrice = value;
                _params.SetOrRemove("minPrice", _minPrice.ToString());
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
                    _params.SetOrRemove("parking", _parking.ToString());
                }
                else
                {
                    _params.Remove("parking");
                }
            }
        }

        public uint Start
        {
            get { return _start; }
            set
            {
                _start = value;
                _params.SetOrRemove("start", _start.ToString());
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
                _params.SetOrRemove("rows", _rows.ToString());
            }
        }

        public EventSearchRequest SortBy(EventSearchSortKey key, SortDirection direction)
        {
            var elem = _sort.FirstOrDefault(n => n.Item1 == key);
            if (elem != null)
            {
                _sort.Remove(elem);
            }
            _sort.Add(new Tuple<EventSearchSortKey, SortDirection>(key, direction));

            return this;
        }

        public string ToQueryString()
        {
            string queryString =  string.Join("&", _params.AllKeys.Select(p => $"{p}={WebUtility.UrlEncode(_params[p])}"));

            for (var i = 0; i < _sort.Count; i++)
            {
                if (i == 0)
                {
                    queryString += $"&sort={_sort[i].Item1} {_sort[i].Item2}";
                    continue;
                }

                queryString += $",{_sort[i].Item1} {_sort[i].Item2}";
            }

            return queryString;
        }

        private readonly NameValueCollection _params;
        private double? _radius;
        private uint? _minTickets;
        private uint? _minPrice;
        private bool? _parking;
        private uint _start;
        private uint? _rows;
        private RadiusUnits _units;
        private readonly List<Tuple<EventSearchSortKey, SortDirection>> _sort;
    }

    public enum RadiusUnits
    {
        Miles,
        Kilometers
    }

    internal static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Sets the value in the collection if the string contains a value. If the value is blank
        /// or null, then the key will be removed from the collection.
        /// </summary>
        /// <param name="collection">The name value collection</param>
        /// <param name="key">They collection key</param>
        /// <param name="value">The value</param>
        public static void SetOrRemove(this NameValueCollection collection, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                collection.Remove(key);
            else
                collection.Add(key, value);
        }
    }

    public sealed class EventSearchSortKey
    {
        public static EventSearchSortKey Popularity = new EventSearchSortKey("popularity");
        public static EventSearchSortKey EventDate = new EventSearchSortKey("eventDateLocal");
        public static EventSearchSortKey Distance = new EventSearchSortKey("distance");

        public EventSearchSortKey(string key)
        {
            _key = key;
        }

        public override string ToString()
        {
            return _key;
        }

        private string _key { get; set; }
    }

    public sealed class SortDirection
    {
        public static SortDirection Ascending = new SortDirection("asc");
        public static SortDirection Descending = new SortDirection("desc");

        private SortDirection(string key)
        {
            _key = key;
        }

        public override string ToString()
        {
            return _key;
        }

        private string _key { get; set; }
    }
}
