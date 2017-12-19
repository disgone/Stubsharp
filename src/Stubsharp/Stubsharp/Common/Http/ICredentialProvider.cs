using System.Threading.Tasks;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{

    public interface ICredentialProvider
    {
        /// <summary>
        /// Gets the credentials
        /// </summary>
        Task<Credentials> GetCredentials();
    }
}
