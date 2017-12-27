using System;
using System.Net;
using Stubsharp.Common.Http;

namespace Stubsharp.Common.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets the HTTP response.
        /// </summary>
        /// <value>The HTTP response.</value>
        public IResponse HttpResponse { get; private set; }

        /// <summary>
        /// The HTTP status code associated with the response
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
    }
}
