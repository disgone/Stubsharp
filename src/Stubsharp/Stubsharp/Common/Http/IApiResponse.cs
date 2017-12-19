using System.Threading.Tasks;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    /// <summary>
    /// A generic API response
    /// </summary>
    /// <typeparam name="T">The content type</typeparam>
    public interface IApiResponse<out T>
    {

        /// <summary>
        /// Gets the deserialized content body.
        /// </summary>
        /// <value>The body.</value>
        T Body { get; }

        /// <summary>
        /// The raw HTTP response
        /// </summary>
        /// <value>The HTTP response.</value>
        IResponse HttpResponse { get; }
    }
}
