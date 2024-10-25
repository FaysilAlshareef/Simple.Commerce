using Simple.Commerce.Application.Contracts.Repositories;

namespace Simple.Commerce.Infra.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        private IProductRepository? _products;

        public IProductRepository Products
        {
            get
            {
                if (_products != null)
                    return _products;

                return _products = new ProductRepository(_appDbContext);
            }
        }


        private IOrderRepository? _orders;

        public IOrderRepository Orders
        {
            get
            {
                if (_orders != null)
                    return _orders;

                return _orders = new OrderRepository(_appDbContext);
            }
        }

        public async Task SaveChangesAsync() => await _appDbContext.SaveChangesAsync();
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
