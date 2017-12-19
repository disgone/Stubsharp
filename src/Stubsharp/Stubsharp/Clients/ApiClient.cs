using Stubsharp.Common.Http;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Clients
{
    public abstract class ApiClient
    {
        protected ApiClient(IConnectionManager connection)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Connection = connection;
        }

        protected IConnectionManager Connection { get; }
    }
}
