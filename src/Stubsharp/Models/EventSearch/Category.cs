using Stubsharp.Models.Base;

namespace Stubsharp.Models.EventSearch
{
    public class Category : NamedItem, IHttpResource
    {
        public string WebUri { get; set; }
    }
}