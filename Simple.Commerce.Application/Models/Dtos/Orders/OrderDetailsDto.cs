using Simple.Commerce.Application.Models.Dtos.Products;

namespace Simple.Commerce.Application.Models.Dtos.Orders
{
    public class OrderDetailsDto : OrderDto
    {
        public IEnumerable<ProductDto> Products { get; set; } = [];
    }
}
