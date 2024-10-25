using Microsoft.AspNetCore.Mvc;
using Simple.Commerce.Application.Contracts.Services;
using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Orders;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Resources;

namespace Simple.Commerce.Api.Controllers.V1
{
    [Route(DefaultRoute)]
    public class OrdersController(IOrderService orderService) : ControllerBaseV1
    {
        private readonly IOrderService _orderService = orderService;

        [HttpGet]
        public async Task<ApiResponse<Paginated<OrderDto>>> GetOrdersAsync([FromQuery] GetOrdersFilterModel filterModel)
            => new ApiResponse<Paginated<OrderDto>>(await _orderService.GetOrdersAsync(filterModel));


        [HttpGet("{id}")]
        public async Task<ApiResponse<OrderDetailsDto>> GetOrderAsync(Guid id)
            => new ApiResponse<OrderDetailsDto>(await _orderService.GetOrderDetailsAsync(id));

        [HttpPost]
        public async Task<ApiResponse<OrderDto>> CreateOrderAsync([FromBody] UpsertOrderDto upsertOrderDto)
            => new ApiResponse<OrderDto>(await _orderService.CreateOrderAsync(upsertOrderDto), string.Format(Phrases.OperationDone, Phrases.Create));

        [HttpPut("{id}")]
        public async Task<ApiResponse<object>> UpdateOrderAsync(Guid id, [FromBody] UpsertOrderDto upsertOrderDto)
        {
            await _orderService.UpdateOrderAsync(id, upsertOrderDto);

            return new ApiResponse<object>(string.Format(Phrases.OperationDone, Phrases.Update));
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<object>> DeleteOrderAsync(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);

            return new ApiResponse<object>(string.Format(Phrases.OperationDone, Phrases.Delete));
        }
    }
}
