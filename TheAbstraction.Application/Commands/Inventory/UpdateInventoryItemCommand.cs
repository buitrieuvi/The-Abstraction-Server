using MediatR;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.Inventory;

public class UpdateInventoryItemCommand : IRequest<int>
{
    public string PlayerId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public class UpdateInventoryItemCommandHandler(IInventoryService inventoryService)
    : IRequestHandler<UpdateInventoryItemCommand, int>
{
    public Task<int> Handle(UpdateInventoryItemCommand request, CancellationToken cancellationToken)
    {
        return inventoryService.Update(request.PlayerId, request.ItemId, request.Quantity, cancellationToken);
    }
}
