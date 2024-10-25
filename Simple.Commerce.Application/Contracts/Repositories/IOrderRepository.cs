using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Orders;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Entities;

namespace Simple.Commerce.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Paginated<OrderDto>> GetOrdersAsync(GetOrdersFilterModel filterModel);
        Task<OrderDetailsDto?> GetOrderDetailsAsync(Guid id);

        Task<bool> AnyAsync(Guid id);
    }
}
