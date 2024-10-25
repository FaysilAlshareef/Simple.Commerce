using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Products;
using Simple.Commerce.Application.Models.FilterModels;

namespace Simple.Commerce.Application.Contracts.Services
{
    public interface IProductService
    {
        Task<Paginated<ProductDto>> GetProductsAsync(GetProductsFilterModel filterModel);
        Task<ProductDetailsDto> GetProductDetailsAsync(Guid id);
        Task<ProductDto> CreateProductAsync(UpsertProductDto dto);
        Task UpdateProductAsync(Guid id, UpsertProductDto dto);
        Task DeleteProductAsync(Guid id);
    }
}
