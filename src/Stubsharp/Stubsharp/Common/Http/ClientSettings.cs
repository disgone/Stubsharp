using Stubsharp.Common.Infrastructure;

namespace Stubsharp
{
    using System.Net.Http.Headers;

    public class ClientSettings
    {
        private readonly ProductHeaderValue _header;

        private ClientSettings(
            string consumerKey, 
            string consumerSecret, 
            ProductHeaderValue productHeader)
        {
            Guard.IsNotNullOrEmpty(consumerKey, nameof(consumerKey));
            Guard.IsNotNullOrEmpty(consumerSecret, nameof(consumerSecret));

            _header = productHeader;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientSettings"/> class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="name">The name of the application.</param>
        public ClientSettings(string consumerKey, string consumerSecret, string name)
            : this(consumerKey, consumerSecret, new ProductHeaderValue(name))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientSettings"/> class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="name">The name of the application.</param>
        /// <param name="version">The version of the application.</param>
        public ClientSettings(string consumerKey, string consumerSecret, string name, string version)
            : this(consumerKey, consumerSecret, new ProductHeaderValue(name, version))
        {
        }

        /// <summary>
        /// Gets the name of the application using the Stubsharp library
        /// </summary>
        public string Name => _header.Name;

        /// <summary>
        /// Gets the version of the application
        /// </summary>
        public string Version => _header.Version;

        /// <summary>
        /// Gets the applications consumer key.
        /// </summary>
        public string ConsumerKey { get; }

        /// <summary>
        /// Gets the applications consumer secret.
        /// </summary>
        public string ConsumerSecret { get; }
    }
}
