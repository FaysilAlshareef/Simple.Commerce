namespace Simple.Commerce.Application.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        Task SaveChangesAsync();
    }
}
