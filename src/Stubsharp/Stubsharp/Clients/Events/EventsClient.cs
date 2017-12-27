using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Stubsharp.Clients.Authorization;
using Stubsharp.Common.Http;
using Stubsharp.Common.Infrastructure;
using Stubsharp.Models.Response;

namespace Stubsharp.Clients.Events
{
    public class EventsClient : ApiClient
    {
        public EventsClient(IConnectionManager connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Gets details about an event
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="locale">The local used to select the translated strings</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>System.Threading.Tasks.Task&lt;Stubsharp.Common.Http.IApiResponse&lt;Stubsharp.Models.Response.Event&gt;&gt;.</returns>
        public Task<IApiResponse<EventDetail>> Get(long eventId, string locale = null, CancellationToken cancellationToken = default)
        {
            IDictionary<string, string> args = null;

            if ( locale.HasValue() )
            {
                args = new Dictionary<string, string>
                {
                    ["locale"] = locale
                };
            }

            return Connection.Get<EventDetail>(Endpoints.EventDetail(eventId), args, null, cancellationToken);
        }

        public Task<IApiResponse<EventSearchResponse>> Search(object searchRequest,
            CancellationToken cancellationToken = default)
        {
            IDictionary<string, string> args = new Dictionary<string, string>
            {
                ["status"] = "active",
                ["point"] = "25.77427,-80.19366"
            };

            return Connection.Get<EventSearchResponse>(Endpoints.EventSearch(), args, null, cancellationToken);
        }
    }
}
