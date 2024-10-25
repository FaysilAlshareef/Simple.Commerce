using Simple.Commerce.Application.Models.Dtos.Orders;

namespace Simple.Commerce.Application.Models.Dtos.Products
{
    public class ProductDetailsDto : ProductDto
    {
        public required OrderDto OrderDto { get; set; }
    }
}
