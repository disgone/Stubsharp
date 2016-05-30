using Stubsharp.Models.Base;

namespace Stubsharp.Models.EventSearch
{
    public class Performer : NamedItem, IHttpResource
    {
        public string WebUri { get; set; }
    }
}