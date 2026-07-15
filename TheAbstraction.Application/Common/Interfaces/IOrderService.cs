using TheAbstraction.Application.Commands.Order.Create;

namespace TheAbstraction.Application.Common.Interfaces;

public interface IOrderService
{
    Task<int> CreateOrderAsync(string userId, List<CreateOrderDetailCommand> orderDetails, CancellationToken cancellationToken = default);
}
