using Newtonsoft.Json;

namespace Mountebank.Models
{
    public class Is
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("body")]
        public object Body { get; set; }
    }
}