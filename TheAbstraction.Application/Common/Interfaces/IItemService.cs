using TheAbstraction.Domain.Entities.NoSQL.Inventory;

namespace TheAbstraction.Application.Common.Interfaces;

public interface IItemService
{
    Task<int> Create(string itemName, CancellationToken cancellationToken = default);
    Task<List<(string id, string itemName)>> GetAll(CancellationToken cancellationToken = default);

}
