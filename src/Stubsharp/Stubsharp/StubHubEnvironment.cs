using System;

namespace Stubsharp
{
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

        public string Scope { get; }
        public Uri BaseUri { get; }
    }
}
