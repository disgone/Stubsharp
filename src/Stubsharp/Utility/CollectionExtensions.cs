using System.Collections.Generic;

namespace Stubsharp.Utility
{
    internal static class CollectionExtensions
    {
        /// <summary>
        /// Sets the value in the collection if the string contains a value. If the value is blank
        /// or null, then the key will be removed from the collection.
        /// </summary>
        /// <param name="collection">The name value collection</param>
        /// <param name="key">They collection key</param>
        /// <param name="value">The value</param>
        public static void SetOrRemove(this Dictionary<string,string> collection, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                collection.Remove(key);
            else
                collection.Add(key, value);
        }
    }
}