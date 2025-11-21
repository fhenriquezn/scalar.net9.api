using Newtonsoft.Json;

namespace scalar.net10.api.Models
{
    public class ProductResponse<T> : DummyResponse
    {
        [JsonProperty("products")]
        public IList<T> Products { get; set; } = [];
    }
}
