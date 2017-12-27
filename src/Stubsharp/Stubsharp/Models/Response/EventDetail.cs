using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Stubsharp.Models.Response
{
    public class EventDetail : IIdentifier<long>
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string EventUrl { get; set; }

        [JsonProperty("integratedEventInd")]
        public bool IsIntegratedEvent { get; set; }

        public string Timezone { get; set; }

        public string CurrencyCode { get; set; }

        public DateTimeOffset EventDateUTC { get; set; }

        public DateTimeOffset EventDateLocal { get; set; }

        public EventStatus EventStatus
        {
            get
            {
                if ( _status == null || !Enum.IsDefined(typeof( EventStatus ), _status.StatusId) )
                {
                    return EventStatus.Unknown;
                }

                return (EventStatus) _status.StatusId;
            }
        }

        public Venue Venue { get; set; }

        public Performers Performers { get; set; }

        public Categories Categories { get; set; }

        public Groupings Groupings { get; set; }

        public EventMetadata EventMeta { get; set; }

        public long GetIdentifier()
        {
            return Id;
        }

        [JsonProperty("status")]
        private EventStatusWrapper _status { get; set; }

        internal class EventStatusWrapper
        {
            public int StatusId { get; set; }
        }
    }

    public class Venue : KeyedName<long>
    {
        [JsonProperty("venueUrl")]
        public string VenueInfoUrl { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string TimeZone { get; set; }

        public GeoLocation Location => new GeoLocation(Latitude, Longitude);

        [JsonProperty("latitude")]
        private float Latitude { get; set; }

        [JsonProperty("longitude")]
        private float Longitude { get; set; }

        [JsonProperty("zipCode")]
        private string _zipCode
        {
            get => PostalCode;
            set => PostalCode = value;
        }
    }

    public class Performers
    {
        public Performer PrimaryPerformer { get; set; }
    }

    public interface IIdentifier<T>
    {
        T Id { get; set; }

        T GetIdentifier();
    }

    public interface INamed
    {
        string Name { get; set; }
    }

    public abstract class KeyedName<T> : IIdentifier<T>, INamed
    {
        public T Id { get; set; }

        public string Name { get; set; }

        public T GetIdentifier()
        {
            return Id;
        }
    }

    public class Performer : KeyedName<long>
    {
        public string WebUri { get; set; }
    }

    public class Categories
    {
        public Category PrimaryCategory { get; set; }
    }

    public class Category : KeyedName<long>
    {
        public string WebUri { get; set; }
    }

    public class Groupings
    {
        public Grouping PrimaryGrouping { get; set; }
    }

    public class Grouping : KeyedName<long>
    {
        public string WebUri { get; set; }
    }

    public class EventMetadata
    {
        public string SEODescription { get; set; }

        public string PrimaryName { get; set; }

        public string SecondaryName { get; set; }

        public string PrimaryAct { get; set; }

        public string SEOTitle { get; set; }

        public string Keywords { get; set; }

        public string Locale { get; set; }
    }

    public enum EventStatus
    {
        Unknown = 0,
        Active = 2,
        Contingent = 3,
        Canceled = 4,
        Completed = 5,
        Postponed = 6,
        Scheduled = 7
    }

    public class EventSearchResponse
    {
        public int NumFound { get; set; }
        public IReadOnlyCollection<EventSearchRecord> Events { get; set; } = new List<EventSearchRecord>();
    }

    public struct GeoLocation : IEquatable<GeoLocation>
    {
        public static readonly GeoLocation Default = new GeoLocation(0, 0);

        public GeoLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; }

        public double Longitude { get; }

        public override string ToString()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(GeoLocation other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if <paramref name="obj">obj</paramref> and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if ( obj is null )
            {
                return false;
            }

            return obj is GeoLocation location && Equals(location);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ( Latitude.GetHashCode() * 397 ) ^ Longitude.GetHashCode();
            }
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:Stubsharp.Models.Response.GeoLocation" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(GeoLocation left, GeoLocation right)
        {
            return left.Equals(right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:Stubsharp.Models.Response.GeoLocation" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(GeoLocation left, GeoLocation right)
        {
            return !left.Equals(right);
        }
    }

    public class EventSearchRecord : KeyedName<long>
    {
        public string Description { get; set; }

        public string WebUri { get; set; }

        public DateTimeOffset EventDateUTC { get; set; }

        public DateTimeOffset EventDateLocal { get; set; }

        public string Locale { get; set; }

        public Venue Venue { get; set; }

        [JsonProperty("categoriesCollection")]
        public IReadOnlyCollection<Category> Categories { get; set; } = new List<Category>();

        [JsonProperty("groupingsCollection")]
        public IReadOnlyCollection<Grouping> Groupings { get; set; } = new List<Grouping>();

        [JsonProperty("performersCollection")]
        public IReadOnlyCollection<Performer> Performers { get; set; } = new List<Performer>();
    }

    public class EventTicketSummary
    {
        [JsonProperty("minPrice")]
        public decimal MinimumPrice { get; set; }

        [JsonProperty("maxPrice")]
        public decimal MaximumPrice { get; set; }

        public int TotalTickets { get; set; }

        public int TotalListings { get; set; }

        public string CurrencyCode { get; set; }
    }
}
