using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    /// <summary>
    /// This is a bit of a shim, but this adds the basic authentication
    /// headers for requesting an access token.
    /// </summary>
    /// <seealso cref="Stubsharp.Common.Http.IAuthenticationHandler" />
    internal class BasicAuthenticationHandler : IAuthenticationHandler
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
            Guard.IsNotNull(credentials.UserId, "ConsumerKey");
            Guard.IsNotNull(credentials.AccessToken, "ConsumerSecret");

            var pair = $"{credentials.UserId}:{credentials.AccessToken}";
            var basicKey = $"Basic {pair.ToBase64()}";

            request.Headers["Authorization"] = basicKey;
        }
    }
}
