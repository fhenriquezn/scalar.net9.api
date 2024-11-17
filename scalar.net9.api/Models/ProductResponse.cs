using Newtonsoft.Json;

namespace scalar.net9.api.Models
{
    public class ProductResponse<T> : DummyResponse
    {
        [JsonProperty("products")]
        public IList<T> Products { get; set; } = [];
    }
}
