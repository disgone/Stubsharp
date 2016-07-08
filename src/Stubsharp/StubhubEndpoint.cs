namespace Stubsharp
{
    internal sealed class StubhubEndpoint
    {
        public static readonly StubhubEndpoint Login = new StubhubEndpoint("Login", "login", false);
        public static readonly StubhubEndpoint EventSearch = new StubhubEndpoint("EventSearch", "search/catalog/events/v3");
        public static readonly StubhubEndpoint InventorySearch = new StubhubEndpoint("InventorySearch", "search/inventory/v2");

        public readonly string Name;
        public readonly string Url;
        public readonly bool RequiresAuthorization;

        public StubhubEndpoint(string name, string url, bool requiresAuthorization = true)
        {
            Name = name;
            Url = url;
            RequiresAuthorization = requiresAuthorization;
        }
    }
}