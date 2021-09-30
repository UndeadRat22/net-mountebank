using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mountebank.Models
{
    public class StubModel
    {
        [JsonProperty("predicates")]
        public List<Predicate> Predicates { get; set; } = new();
        [JsonProperty("responses")]
        public List<Response> Responses { get; set; } = new();
    }
}