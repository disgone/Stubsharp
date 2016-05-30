using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Stubsharp.Models.Base
{
    public abstract class NamedItem : IIdentifier<int>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
