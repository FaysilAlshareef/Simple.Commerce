using Simple.Commerce.Application.Contracts.Repositories;
using Simple.Commerce.Application.Contracts.Services;
using Simple.Commerce.Application.Models;
using Simple.Commerce.Application.Models.Dtos.Orders;
using Simple.Commerce.Application.Models.FilterModels;
using Simple.Commerce.Domain.Entities;
using Simple.Commerce.Domain.Exceptions;
using Simple.Commerce.Domain.Resources;

namespace Simple.Commerce.Infra.Services.BaseServices
{
    public class OrderService(IUnitOfWork unitOfWork) : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OrderDto> CreateOrderAsync(UpsertOrderDto dto)
        {
            var order = new Order(dto.CustomerId);

            await _unitOfWork.Orders.AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return new OrderDto
            {
                Id = order.Id,
                CustomerId = dto.CustomerId,
                OrderDate = order.OrderDate,
                TotolAmount = order.TotolAmount
            };
        }
        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _unitOfWork.Orders.FindAsync(id, true)
                ?? throw new AppException(System.Net.HttpStatusCode.NotFound, Phrases.OrderNotFound);

            if (order.Products.Any())
                throw new AppException(System.Net.HttpStatusCode.Forbidden, Phrases.DeleteOrderIsForbidden);

            await _unitOfWork.Orders.RemoveAsync(order);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<OrderDetailsDto> GetOrderDetailsAsync(Guid id)
        {
            var order = await _unitOfWork.Orders.GetOrderDetailsAsync(id)
                 ?? throw new AppException(System.Net.HttpStatusCode.NotFound, Phrases.OrderNotFound);

            return order;
        }
        public async Task<Paginated<OrderDto>> GetOrdersAsync(GetOrdersFilterModel filterModel)
            => await _unitOfWork.Orders.GetOrdersAsync(filterModel);
        public async Task UpdateOrderAsync(Guid id, UpsertOrderDto dto)
        {
            var order = await _unitOfWork.Orders.FindAsync(id)
               ?? throw new AppException(System.Net.HttpStatusCode.NotFound, Phrases.OrderNotFound);

            order.Update(dto.CustomerId);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
