using Microsoft.AspNetCore.Mvc;
using Simple.Commerce.Application.Contracts.Services;
using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Products;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Resources;

namespace Simple.Commerce.Api.Controllers.V1
{

    [Route(DefaultRoute)]
    public class ProductsController(IProductService productService) : ControllerBaseV1
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<ApiResponse<Paginated<ProductDto>>> GetProductsAsync([FromQuery] GetProductsFilterModel filterModel)
            => new ApiResponse<Paginated<ProductDto>>(await _productService.GetProductsAsync(filterModel));


        [HttpGet("{id}")]
        public async Task<ApiResponse<ProductDetailsDto>> GetProductAsync(Guid id)
            => new ApiResponse<ProductDetailsDto>(await _productService.GetProductDetailsAsync(id));

        [HttpPost]
        public async Task<ApiResponse<ProductDto>> CreateProductAsync([FromBody] UpsertProductDto upsertProductDto)
            => new ApiResponse<ProductDto>(await _productService.CreateProductAsync(upsertProductDto), string.Format(Phrases.OperationDone, Phrases.Create));

        [HttpPut("{id}")]
        public async Task<ApiResponse<object>> UpdateProductAsync(Guid id, [FromBody] UpsertProductDto upsertProductDto)
        {
            await _productService.UpdateProductAsync(id, upsertProductDto);

            return new ApiResponse<object>(string.Format(Phrases.OperationDone, Phrases.Update));
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<object>> DeleteProductAsync(Guid id)
        {
            await _productService.DeleteProductAsync(id);

            return new ApiResponse<object>(string.Format(Phrases.OperationDone, Phrases.Delete));
        }
    }
}
