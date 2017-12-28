using System;
using Newtonsoft.Json;

namespace RamowkiWithOpenIddictAuth.Models
{
    public abstract class Day {
        [JsonProperty(Required = Required.AllowNull)]
        public Guid Id { get; set; }
    }
}
