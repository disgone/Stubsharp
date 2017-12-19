using System;

namespace Stubsharp.Common.Http
{
    public class ResponseMetadata
    {
        internal ResponseMetadata()
        {
        }

        public ResponseMetadata(string userId)
        {
            UserId = userId;
        }

        [Metadata(MetadataProperty = "X-StubHub-User-GUID")]
        public string UserId { get; }
    }

    
    [AttributeUsage(AttributeTargets.Property)]
    internal class MetadataAttribute : Attribute
    {
        public string MetadataProperty { get; set; }
    }
}
