using Newtonsoft.Json;

namespace scalar.net10.api.Models.v1
{
    [JsonObject]
    public record Product(
         int Id,
         string Title,
         string Description,
         string Category,
         double Price,
         double DiscountPercentage,
         double Rating
    );
}
