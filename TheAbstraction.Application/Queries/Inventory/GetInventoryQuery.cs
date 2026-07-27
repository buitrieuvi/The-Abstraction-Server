using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.Inventory;

public class GetInventoryQuery : IRequest<List<InventoryResponseDTO>>;

public class GetInventoryQueryHandler(IInventoryService inventoryService)
    : IRequestHandler<GetInventoryQuery, List<InventoryResponseDTO>>
{
    public Task<List<InventoryResponseDTO>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
    {
        return inventoryService.Get(cancellationToken);
    }
}
