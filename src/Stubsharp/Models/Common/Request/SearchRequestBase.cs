using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Stubsharp.Models.Common.Request
{
    public abstract class SearchRequestBase
    {
        protected Dictionary<string,string> Params;
        protected List<Tuple<ISearchSortKey, SortDirection>> Sort;

        protected SearchRequestBase()
        {
            Params = new Dictionary<string, string>();
            Sort = new List<Tuple<ISearchSortKey, SortDirection>>();
        }

        protected virtual SearchRequestBase SortBy(ISearchSortKey key, SortDirection direction)
        {
            var elem = Sort.FirstOrDefault(n => n.Item1 == key);
            if (elem != null)
            {
                Sort.Remove(elem);
            }
            Sort.Add(new Tuple<ISearchSortKey, SortDirection>(key, direction));

            return this;
        }

        public string ToQueryString()
        {
            string queryString =  string.Join("&", Params.Keys.Select(p => $"{p}={WebUtility.UrlEncode(Params[p])}"));

            for (var i = 0; i < Sort.Count; i++)
            {
                var sortKey = $"{Sort[i].Item1} {Sort[i].Item2}";
                if (i == 0)
                {
                    queryString += "&sort=";
                }
                else
                {
                    queryString += ",";
                }

                queryString += WebUtility.UrlEncode(sortKey);
            }

            return queryString;
        }
    }
}