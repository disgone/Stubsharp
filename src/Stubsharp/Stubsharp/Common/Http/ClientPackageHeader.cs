namespace Stubsharp
{
    using System.Net.Http.Headers;

    public class ClientPackageHeader
    {
        readonly ProductHeaderValue _header;

        public ClientPackageHeader(string name)
            : this(new ProductHeaderValue(name))
        {
        }

        public ClientPackageHeader(string name, string version)
            : this(new ProductHeaderValue(name, version))
        {
        }

        ClientPackageHeader(ProductHeaderValue productHeader)
        {
            _header = productHeader;
        }

        /// <summary>
        /// Gets the name of the application using the Stubsharp library
        /// </summary>
        public string Name => _header.Name;

        /// <summary>
        /// Gets the version of the application
        /// </summary>
        public string Version => _header.Version;
    }
}
