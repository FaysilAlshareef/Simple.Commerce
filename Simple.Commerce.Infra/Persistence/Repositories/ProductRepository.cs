using Microsoft.EntityFrameworkCore;
using Simple.Commerce.Application.Contracts.Repositories;
using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Orders;
using Simple.Commerce.Application.Models.Dtos.Products;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Entities;

namespace Simple.Commerce.Infra.Persistence.Repositories
{
    public class ProductRepository(AppDbContext appDbContext) : GenericRepository<Product>(appDbContext), IProductRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<ProductDetailsDto?> GetProductDetailsAsync(Guid id)
          => await _appDbContext.Products.Where(p => p.Id == id)
            .Select(p => new ProductDetailsDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OrderId = p.OrderId,
                Price = p.Price,
                Stock = p.Stock,
                OrderDto = new OrderDto
                {
                    Id = p.Order!.Id,
                    CustomerId = p.Order.CustomerId,
                    OrderDate = p.Order.OrderDate,
                    TotolAmount = p.Order.TotolAmount
                }
            }).FirstOrDefaultAsync();

        public async Task<Paginated<ProductDto>> GetProductsAsync(GetProductsFilterModel filterModel)
        {
            var skip = (filterModel.CurrentPage - 1) * filterModel.PageSize;

            var data = _appDbContext.Products.AsNoTracking().AsQueryable();

            if (filterModel.OrderId.HasValue)
                data = data.Where(p => p.OrderId == filterModel.OrderId.Value);

            if (!string.IsNullOrEmpty(filterModel.Name))
                data = data.Where(p => p.Name.Contains(filterModel.Name));

            var total = data.Count();

            var products = await data.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OrderId = p.OrderId,
                Price = p.Price,
                Stock = p.Stock,
            }).Skip(skip).Take(filterModel.PageSize).ToListAsync();

            return new Paginated<ProductDto>
            {
                CurrentPage = filterModel.CurrentPage,
                PageSize = filterModel.PageSize,
                Results = products,
                Total = total,
            };
        }
    }
}
