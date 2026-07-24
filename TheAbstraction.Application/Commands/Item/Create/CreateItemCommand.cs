using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Commands.Order.Create;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.InventoryItem.Create
{
    public class CreateItemCommand : IRequest<int>
    {
        public string ItemName { get; set; }
    }

    public class CreateInventoryItemCommandHandle(
        IItemService inventoryItem,
        IHttpContextAccessor httpContextAccessor

    ) : IRequestHandler<CreateItemCommand, int>
    {
        private readonly IItemService _inventoryItemService = inventoryItem;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            return await _inventoryItemService.Create(request.ItemName);
        }
    }
}
