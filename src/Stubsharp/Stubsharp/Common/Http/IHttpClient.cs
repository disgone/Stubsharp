using System;
using System.Threading;
using System.Threading.Tasks;
using Stubsharp.Common.Http;

namespace Stubsharp
{
    /// <summary>
    /// A generic HTTP client.
    /// </summary>
    public interface IHttpClient : IDisposable
    {
        /// <summary>
        /// Sends the specified request and returns a response.
        /// </summary>
        /// <param name="request">A <see cref="IRequest"/> that represents the HTTP request</param>
        /// <param name="cancellationToken">Used to cancel the request</param>
        /// <returns>A <see cref="Task" /> of <see cref="IResponse"/></returns>
        Task<IResponse> Send(IRequest request, CancellationToken cancellationToken);
    }
}
