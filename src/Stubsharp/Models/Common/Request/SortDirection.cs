namespace Stubsharp.Models.Common.Request
{
    public sealed class SortDirection
    {
        public static SortDirection Ascending = new SortDirection("asc");
        public static SortDirection Descending = new SortDirection("desc");

        private SortDirection(string key)
        {
            _key = key;
        }

        public override string ToString()
        {
            return _key;
        }

        private string _key { get; set; }
    }
}