using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.Item;

public class GetItemsQuery : IRequest<List<ItemResponseDTO>>;

public class GetItemsQueryHandler(IItemService itemService) : IRequestHandler<GetItemsQuery, List<ItemResponseDTO>>
{
    public async Task<List<ItemResponseDTO>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await itemService.GetAll(cancellationToken);
        return items.Select(item => new ItemResponseDTO
        {
            Id = item.id,
            ItemName = item.itemName
        }).ToList();
    }
}
