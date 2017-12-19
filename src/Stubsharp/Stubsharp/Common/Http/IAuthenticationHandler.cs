namespace Stubsharp.Common.Http
{
    internal interface IAuthenticationHandler
    {
        /// <summary>
        /// Adds authentication to the provided request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="credentials">Then authentication credentials</param>
        void Authenticate(IRequest request, Credentials credentials);
    }
}
