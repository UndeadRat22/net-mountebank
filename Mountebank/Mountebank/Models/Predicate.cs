using Newtonsoft.Json;

namespace Mountebank.Models
{
    public class Predicate
    {
        [JsonProperty("deepEquals")]
        public DeepEquals DeepEquals { get; set; }
    }
}