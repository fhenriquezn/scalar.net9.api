namespace scalar.net9.api.Models.v2
{
    public partial record Product(
         int Id,
         string Title,
         string Description,
         string Category,
         double Price,
         double DiscountPercentage,
         double Rating,
         int Stock,
         IReadOnlyList<string> Tags,
         string Brand,
         string Sku,
         int Weight,
         string WarrantyInformation,
         string ShippingInformation,
         string AvailabilityStatus,
         string ReturnPolicy,
         int MinimumOrderQuantity,
         string Thumbnail,
         IReadOnlyList<string> Images
    );
}
