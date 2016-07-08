namespace Stubsharp
{
    public sealed class StubhubEnvironment
    {
        public static readonly StubhubEnvironment Production = new StubhubEnvironment("PRODUCTION", "api.stubhub.com");
        public static readonly StubhubEnvironment Sandbox = new StubhubEnvironment("SANDBOX", "api.stubhubsandbox.com");

        public readonly string Name;
        public readonly string Domain;
        
        private StubhubEnvironment(string name, string domain)
        {
            Name = name;
            Domain = domain;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}