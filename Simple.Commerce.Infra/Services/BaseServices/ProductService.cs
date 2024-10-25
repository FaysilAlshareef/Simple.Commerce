using Simple.Commerce.Application.Contracts.Repositories;
using Simple.Commerce.Application.Contracts.Services;
using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Products;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Entities;
using Simple.Commerce.Domain.Exceptions;
using Simple.Commerce.Domain.Resources;

namespace Simple.Commerce.Infra.Services.BaseServices
{
    public class ProductService(IUnitOfWork unitOfWork) : IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ProductDto> CreateProductAsync(UpsertProductDto dto)
        {
            var product = new Product(dto);

            await _unitOfWork.Products.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                OrderId = product.OrderId,
                Price = product.Price,
                Stock = product.Stock,
            };
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.Products.FindAsync(id)
                ?? throw new AppException(System.Net.HttpStatusCode.NotFound, Phrases.ProductNotFound);

            await _unitOfWork.Products.RemoveAsync(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductDetailsDto> GetProductDetailsAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetProductDetailsAsync(id)
                ?? throw new AppException(System.Net.HttpStatusCode.NotFound, Phrases.ProductNotFound);

            return product;
        }

        public async Task<Paginated<ProductDto>> GetProductsAsync(GetProductsFilterModel filterModel)
             => await _unitOfWork.Products.GetProductsAsync(filterModel);
        public async Task UpdateProductAsync(Guid id, UpsertProductDto dto)
        {
            var product = await _unitOfWork.Products.FindAsync(id)
               ?? throw new AppException(System.Net.HttpStatusCode.NotFound, Phrases.ProductNotFound);

            var order = await _unitOfWork.Orders.FindAsync(dto.OrderId)
             ?? throw new AppException(System.Net.HttpStatusCode.NotFound, Phrases.OrderNotFound);

            product.Update(dto);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
