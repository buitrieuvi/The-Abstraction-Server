namespace TheAbstraction.Application.Common.Interfaces;

public interface IOrderDetailService
{
    Task<int> CreateOrderDetailAsync(string orderId, string productVariantId, int quantity, CancellationToken cancellationToken = default);
}
