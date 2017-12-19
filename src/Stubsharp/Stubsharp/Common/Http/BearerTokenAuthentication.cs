using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    /// <summary>
    /// Authentication handler for using bearer token authentication
    /// </summary>
    /// <seealso cref="Stubsharp.Common.Http.IAuthenticationHandler" />
    internal class BearerTokenAuthentication : IAuthenticationHandler
    {
        /// <summary>
        /// Adds authentication to the provided request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="credentials">Then authentication credentials</param>
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Guard.IsNotNull(request, nameof(request));
            Guard.IsNotNull(credentials, nameof(credentials));
            Guard.IsNotNull(credentials.AccessToken, nameof(credentials.AccessToken));

            request.Headers["Authorization"] = $"Bearer {credentials.AccessToken}";
        }
    }
}
