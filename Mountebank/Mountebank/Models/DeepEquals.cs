using Newtonsoft.Json;

namespace Mountebank.Models
{
    public class DeepEquals
    {
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}