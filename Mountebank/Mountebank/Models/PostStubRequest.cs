using Newtonsoft.Json;

namespace Mountebank.Models
{
    public record PostStubRequest
    {
        [JsonProperty("stub")]
        public StubModel Stub { get; set; }
    };
}