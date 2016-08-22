using Stubsharp.Models.Common.Response;

namespace Stubsharp.Models.EventSearch.Response
{
    public class Performer : NamedItem, IHttpResource
    {
        public string WebUri { get; set; }
    }
}