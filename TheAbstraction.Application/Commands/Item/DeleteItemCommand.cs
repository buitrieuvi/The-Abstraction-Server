using MediatR;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.Item;

public class DeleteItemCommand : IRequest<int>
{
    public string ItemId { get; set; } = string.Empty;
}

public class DeleteItemCommandHandler(IItemService itemService) : IRequestHandler<DeleteItemCommand, int>
{
    private readonly IItemService _itemService = itemService;

    public Task<int> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        return _itemService.Delete(request.ItemId, cancellationToken);
    }
}
