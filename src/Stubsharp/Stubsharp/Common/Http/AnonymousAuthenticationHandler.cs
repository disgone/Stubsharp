using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{

    internal class AnonymousAuthenticationHandler : IAuthenticationHandler
    {
        /// <summary>
        /// Adds authentication to the provided request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="credentials">Then authentication credentials</param>
        public void Authenticate(IRequest request, Credentials credentials)
        {
            // Do nothing
        }
    }
}
