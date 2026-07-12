using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Common.Interfaces
{
    public interface IProductVariantService
    {
        Task<IReadOnlyList<ProductVariantResponseDTO>> GetProductVariantAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductVariantResponseDTO>> GetProductVariantByProductIdAsync(string productId, CancellationToken cancellationToken = default);
        Task<int> CreateProductVariantAsync(string productId, decimal price, int quantity,
            string model,
            string color,
            string size,
            CancellationToken cancellationToken = default);
        Task<int> DeleteProductVariantAsync(string id, CancellationToken cancellationToken = default);
        Task<int> UpdateProductVariantAsync(string id, decimal price, int quantity, bool isActive,
            string model,
            string color,
            string size,
            CancellationToken cancellationToken = default);
    }
}