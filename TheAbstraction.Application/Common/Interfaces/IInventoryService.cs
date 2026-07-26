using TheAbstraction.Application.DTOs;
using TheAbstraction.Domain.Entities.NoSQL.Inventory;

namespace TheAbstraction.Application.Common.Interfaces
{
    public interface IInventoryService
    {
        Task<int> Create(string playerId, CancellationToken cancellationToken = default);
        Task<int> Update(string playerId, string itemId, int newQuantity, CancellationToken cancellationToken = default);
        Task<int> Update(string playerId, List<InventorySlot> newItems, CancellationToken cancellationToken = default);

        Task<List<InventoryResponseDTO>> Get(CancellationToken cancellationToken = default);
        Task<int> DeleteInventoryByUserId(string userId, CancellationToken cancellationToken = default);
    }
}

