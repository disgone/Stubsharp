using Stubsharp.Models.Common.Response;

namespace Stubsharp.Models.EventSearch.Response
{
    public class Grouping : NamedItem, IHttpResource
    {
        public string WebUri { get; set; }
    }
}