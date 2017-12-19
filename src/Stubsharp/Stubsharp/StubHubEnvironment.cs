using System;

namespace Stubsharp
{
    /// <summary>
    /// StubHub provides two environments for developers, production and a sandbox.
    /// This class represents the configuration information needed for both environments.
    /// </summary>
    public class StubHubEnvironment
    {
        /// <summary>
        /// StubHub production environment
        /// </summary>
        public static StubHubEnvironment Production = new StubHubEnvironment("PRODUCTION", new Uri("https://api.stubhub.com/"));

        /// <summary>
        /// StubHub sandbox environment
        /// </summary>
        public static StubHubEnvironment Sandbox = new StubHubEnvironment("SANDBOX", new Uri("https://api.stubhubsandbox.com/"));

        private StubHubEnvironment(string scope, Uri baseUri)
        {
            Scope = scope;
            BaseUri = baseUri;
        }

        /// <summary>
        /// Gets name of the scope for the environment
        /// </summary>
        /// <value>The scope.</value>
        public string Scope { get; }

        /// <summary>
        /// Gets the base URI for the environment.
        /// </summary>
        /// <value>The base URI.</value>
        public Uri BaseUri { get; }
    }
}
