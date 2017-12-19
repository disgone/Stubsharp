using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{

    internal class Response : IResponse
    {
        public Response()
            : this(new Dictionary<string, string>())
        {
        }

        public Response(IDictionary<string, string> headers)
        {
            Guard.IsNotNull(headers, nameof(headers));
            Headers = new ReadOnlyDictionary<string, string>(headers);
            Metadata = ResponseMetadataParser.Parse(headers);
        }

        public Response(HttpStatusCode statusCode, object body, IDictionary<string, string> headers, string contentType)
        {
            Guard.IsNotNull(headers, nameof(headers));

            StatusCode = statusCode;
            Body = body;
            Headers = new ReadOnlyDictionary<string, string>(headers);
            ContentType = contentType;
            Metadata = ResponseMetadataParser.Parse(headers);
        }

        /// <summary>
        /// Gets the response body
        /// </summary>
        /// <value>The body.</value>
        public object Body { get; }

        /// <summary>
        /// Gets the response headers.
        /// </summary>
        /// <value>The headers.</value>
        public IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets the HTTP status code returned.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The content type of the response.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Information about the API response parsed from the response headers.
        /// </summary>
        public ResponseMetadata Metadata { get; internal set; }
    }
}
