namespace Stubsharp
{
    internal sealed class StubhubEndpoint
    {
        public static readonly StubhubEndpoint Login = new StubhubEndpoint("Login", "login", false);
        public static readonly StubhubEndpoint EventSearchV3 = new StubhubEndpoint("EventSearch-V3", "search/catalog/events/v3");
        public static readonly StubhubEndpoint InventorySearchV1 = new StubhubEndpoint("InventorySearch-V1", "search/inventory/v1");
        public static readonly StubhubEndpoint InventorySearchV2 = new StubhubEndpoint("InventorySearch-V2", "search/inventory/v2");
        

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