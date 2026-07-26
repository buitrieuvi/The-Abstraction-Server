using TheAbstraction.Application.Commands.Order;
using TheAbstraction.Domain.Entities;

namespace TheAbstraction.Application.Common.Interfaces;

public interface IOrderService
{
    Task<int> CreateOrderAsync(string userId, List<CreateOrderDetailCommand> orderDetails, CancellationToken cancellationToken = default);
    Task<int> UpdateOrderStatus(string orderId, string newStatus, CancellationToken cancellationToken = default);
}
