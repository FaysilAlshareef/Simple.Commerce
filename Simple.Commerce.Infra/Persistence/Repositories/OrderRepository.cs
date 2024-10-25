using Microsoft.EntityFrameworkCore;
using Simple.Commerce.Application.Contracts.Repositories;
using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Orders;
using Simple.Commerce.Application.Models.Dtos.Products;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Entities;

namespace Simple.Commerce.Infra.Persistence.Repositories
{

    public class OrderRepository(AppDbContext appDbContext) : GenericRepository<Order>(appDbContext), IOrderRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public Task<bool> AnyAsync(Guid id)
            => _appDbContext.Orders.AnyAsync(o => o.Id == id);

        public async Task<OrderDetailsDto?> GetOrderDetailsAsync(Guid id)
        {
            return await _appDbContext.Orders.Where(o => o.Id == id)
                .Select(o => new OrderDetailsDto
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    OrderDate = o.OrderDate,
                    TotolAmount = o.Products.Sum(p => p.Price * p.Stock),
                    Products = o.Products.Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        OrderId = p.OrderId,
                        Price = p.Price,
                        Stock = p.Stock
                    })
                }).FirstOrDefaultAsync();
        }

        public async Task<Paginated<OrderDto>> GetOrdersAsync(GetOrdersFilterModel filterModel)
        {
            var skip = (filterModel.CurrentPage - 1) * filterModel.PageSize;

            var data = _appDbContext.Orders.AsNoTracking().AsQueryable();

            if (filterModel.CustomerId.HasValue)
                data = data.Where(o => o.CustomerId == filterModel.CustomerId.Value);

            if (filterModel.OrderDate.HasValue)
                data = data.Where(o => o.OrderDate == filterModel.OrderDate.Value);

            var total = data.Count();

            var orders = await data.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                TotolAmount = o.Products.Sum(p => p.Price * p.Stock)
            }).Skip(skip).Take(filterModel.PageSize).ToListAsync();

            return new Paginated<OrderDto>()
            {
                CurrentPage = filterModel.CurrentPage,
                PageSize = filterModel.PageSize,
                Results = orders,
                Total = total
            };
        }

        public override Task<Order?> FindAsync(Guid id, bool includeRelated = false)
        {
            if (includeRelated)
                return _appDbContext.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);

            return base.FindAsync(id, includeRelated);
        }
    }
}
