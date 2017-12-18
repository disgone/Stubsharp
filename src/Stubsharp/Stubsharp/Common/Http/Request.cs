using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Stubsharp.Common.Http
{
    /// <summary>
    /// A generic HTTP request
    /// </summary>
    /// <seealso cref="IRequest" />
    public class Request : IRequest
    {
        public Request()
        {
            Parameters = new Dictionary<string, string>();
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the body of the request
        /// </summary>
        /// <value>The body.</value>
        public object Body { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method
        /// </summary>
        /// <value>The HTTP method.</value>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// Gets or sets the request parameters
        /// </summary>
        /// <value>The parameters.</value>
        public Dictionary<string, string> Parameters { get; private set; }

        /// <summary>
        /// Gets or sets the request base address.
        /// </summary>
        /// <value>The base address.</value>
        public Uri BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the endpoint of the request
        /// </summary>
        /// <value>The endpoint.</value>
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the request headers.
        /// </summary>
        /// <value>The headers.</value>
        public Dictionary<string, string> Headers { get; private set; }
    }
}
