using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Stubsharp.Common.Http
{
    /// <summary>
    /// A generic HTTP request
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Gets or sets the body of the request
        /// </summary>
        /// <value>The body.</value>
        object Body { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method
        /// </summary>
        /// <value>The HTTP method.</value>
        HttpMethod Method { get; }

        /// <summary>
        /// Gets or sets the request parameters
        /// </summary>
        /// <value>The parameters.</value>
        Dictionary<string,string> Parameters { get; }

        /// <summary>
        /// Gets or sets the request base address.
        /// </summary>
        /// <value>The base address.</value>
        Uri BaseAddress { get; }

        /// <summary>
        /// Gets or sets the endpoint of the request
        /// </summary>
        /// <value>The endpoint.</value>
        string Endpoint { get; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        string ContentType { get; }

        /// <summary>
        /// Gets or sets the request headers.
        /// </summary>
        /// <value>The headers.</value>
        Dictionary<string,string> Headers { get; }
    }
}
