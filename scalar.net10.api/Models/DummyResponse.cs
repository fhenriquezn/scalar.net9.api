using Newtonsoft.Json;

namespace scalar.net10.api.Models
{
    [JsonObject]
    public abstract class DummyResponse
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("skip")]
        public int Skip { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }
    }
}
