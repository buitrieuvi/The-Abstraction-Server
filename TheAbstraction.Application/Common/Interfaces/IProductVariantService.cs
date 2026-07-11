namespace TheAbstraction.Application.Common.Interfaces
{
    public interface IProductVariantService
    {
        Task<int> CreateAsync(string productId, decimal price, int quantity, CancellationToken cancellationToken = default);
    }
}