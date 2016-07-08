using System;
using Newtonsoft.Json;

namespace Stubsharp
{
    public class StubsharpAuthentication
    {
        public Guid UserId { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpireTime { get; set; }

        public DateTime? TokenExpirationDate => _requestDate.AddSeconds(ExpireTime);

        public StubsharpAuthentication()
        {
            _requestDate = DateTime.UtcNow;
        }

        private readonly DateTime _requestDate;
    }
}