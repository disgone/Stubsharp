namespace Stubsharp.Models.Common.Request
{
    public interface ISearchSortKey
    {
        /// <summary>
        /// The name of the property to sort
        /// </summary>
        /// <value>The sort key.</value>
        string SortKey { get; }
    }
}