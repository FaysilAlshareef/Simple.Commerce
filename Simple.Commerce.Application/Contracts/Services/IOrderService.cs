using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Orders;
using Simple.Commerce.Application.Models.FilterModels;

namespace Simple.Commerce.Application.Contracts.Services
{
    public interface IOrderService
    {
        Task<Paginated<OrderDto>> GetOrdersAsync(GetOrdersFilterModel filterModel);
        Task<OrderDetailsDto> GetOrderDetailsAsync(Guid id);
        Task<OrderDto> CreateOrderAsync(UpsertOrderDto dto);
        Task UpdateOrderAsync(Guid id, UpsertOrderDto dto);
        Task DeleteOrderAsync(Guid id);
    }
}
