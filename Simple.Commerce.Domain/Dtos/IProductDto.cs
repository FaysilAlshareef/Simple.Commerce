namespace Simple.Commerce.Domain.Dtos
{
    public interface IProductDto
    {
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
        int Stock { get; }
        Guid OrderId { get; }
    }
}
