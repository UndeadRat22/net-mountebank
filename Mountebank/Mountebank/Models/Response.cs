using Newtonsoft.Json;

namespace Mountebank.Models
{
    public class Response
    {
        [JsonProperty("is")]
        public Is Is { get; set; }
    }
}