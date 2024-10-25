namespace Simple.Commerce.Application.Models.Dtos.Products
{
    public class ProductDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required Guid OrderId { get; set; }
    }
}
