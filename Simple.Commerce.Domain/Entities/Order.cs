namespace Simple.Commerce.Domain.Entities
{
    public class Order
    {
        private Order(Guid id, Guid customerId, DateTime orderDate)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
        }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public List<Product> Products { get; private set; } = [];
        public decimal TotolAmount => Products.Sum(i => i.Price * i.Stock);

        public void Update(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
