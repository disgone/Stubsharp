using System.Collections.Generic;
using System.Reflection;
using Stubsharp.Common.Infrastructure;

namespace Stubsharp.Common.Http
{
    internal static class ResponseMetadataParser
    {
        public static ResponseMetadata Parse(IDictionary<string, string> responseHeaders)
        {
            ResponseMetadata data = new ResponseMetadata();

            if ( responseHeaders != null )
            {
                foreach (PropertyInfo propertyInfo in typeof(ResponseMetadata).GetProperties())
                {
                    string propertyName = propertyInfo.Name;

                    object[] attribute = propertyInfo.GetCustomAttributes(typeof(MetadataAttribute), true);

                    if (attribute.Length > 0)
                    {
                        MetadataAttribute attr = (MetadataAttribute)attribute[0];

                        if ( attr.MetadataProperty.HasValue() && responseHeaders.ContainsKey(attr.MetadataProperty) )
                        {
                            data.GetType().GetProperty(propertyName)
                                .SetValue(data, responseHeaders["X-StubHub-User-GUID"], null);
                        }
                    }
                }
            }

            return data;
        }
    }
}
