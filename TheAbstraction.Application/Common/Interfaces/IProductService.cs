#nullable enable
using TheAbstraction.Application.Commands.ProductVariant;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateAsync(string name, string? description, List<CreateProductVariantCommand> productVariants, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(string id, string name, string? description, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default);

        Task<ProductResponseDTO> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProductResponseDTO>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
