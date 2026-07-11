using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateAsync(string name, string description, int stockQuantity, bool isActive, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(string id, string name, string description, int stockQuantity, bool isActive, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default);

        Task<ProductResponseDTO> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
