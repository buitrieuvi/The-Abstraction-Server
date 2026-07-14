using TheAbstraction.Application.Commands.Order.Create;

namespace TheAbstraction.Application.Common.Interfaces;

public interface IOrderService
{
    Task<int> CreateOrderAsync(CreateOrderCommand order, CancellationToken cancellationToken = default);
}
