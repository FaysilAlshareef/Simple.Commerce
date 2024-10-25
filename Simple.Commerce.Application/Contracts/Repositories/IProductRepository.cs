using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Products;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Entities;

namespace Simple.Commerce.Application.Contracts.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ProductDetailsDto?> GetProductDetailsAsync(Guid id);
        Task<Paginated<ProductDto>> GetProductsAsync(GetProductsFilterModel filterModel);
    }
}
