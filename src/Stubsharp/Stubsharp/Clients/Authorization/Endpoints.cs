using System;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Clients.Authorization
{
    internal static class Endpoints
    {
        public static readonly Uri AccessToken = new Uri("login", UriKind.Relative);
        public static readonly Uri InventorySearchV2 = new Uri("search/inventory/v1", UriKind.Relative);

        public static Uri EventSearch(EventSearchVersion version = EventSearchVersion.v3)
        {
            return "search/catalog/events/{0}".FormatUri(version.ToString("G"));
        }

        public static Uri InventorySearch(InventorySearchVersion version = InventorySearchVersion.v2)
        {
            return "search/inventory/{0}".FormatUri(version.ToString("G"));
        }

        public static Uri EventDetail(long eventId)
        {
            return "catalog/events/v2/{0}".FormatUri(eventId);
        }
    }

    /// <summary>
    /// Supported event search versions
    /// </summary>
    internal enum EventSearchVersion
    {
        v2,
        v3
    }

    /// <summary>
    /// Supported inventory search versions
    /// </summary>
    internal enum InventorySearchVersion
    {
        v1,
        v2
    }
}
