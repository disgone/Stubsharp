namespace Stubsharp
{
    public class StubsharpClient
    {
        public StubsharpClient(
            ClientPackageHeader package, 
            StubHubEnvironment environment)
        {
            
        }
    }

    public interface IConnectionHandler
    {

    }

    public class Credentials
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
