namespace Simple.Commerce.Application.Models.Dtos.Orders
{
    public class OrderDto
    {
        public required Guid Id { get; set; }
        public required Guid CustomerId { get; set; }
        public required DateTime OrderDate { get; set; }
        public required decimal TotolAmount { get; set; }
    }
}
