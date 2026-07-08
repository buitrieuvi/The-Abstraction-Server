using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateAsync(string name, string? description, decimal price, int stockQuantity, bool isActive, CancellationToken cancellationToken = default);
        Task<ProductResponseDTO?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(string id, string name, string? description, decimal price, int stockQuantity, bool isActive, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
