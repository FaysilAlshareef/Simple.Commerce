using Simple.Commerce.Domain.Dtos;

namespace Simple.Commerce.Domain.Entities
{
    public class Product
    {
        private Product(
            Guid id,
            string name,
            string description,
            decimal price,
            int stock,
            Guid orderId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            OrderId = orderId;
        }

        public Product(IProductDto productDto)
        {
            Name = productDto.Name;
            Description = productDto.Description;
            Price = productDto.Price;
            Stock = productDto.Stock;
            OrderId = productDto.OrderId;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }
        public void Update(IProductDto productDto)
        {
            Name = productDto.Name;
            Description = productDto.Description;
            Price = productDto.Price;
            Stock = productDto.Stock;
            OrderId = productDto.OrderId;
        }
    }
}
