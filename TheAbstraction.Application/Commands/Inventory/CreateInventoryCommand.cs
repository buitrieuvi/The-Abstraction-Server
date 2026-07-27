using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Commands.Auth;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Commands.Inventory
{
    public class CreateInventoryCommand : IRequest<int>
    {
        public string PlayerId { get; set; } = string.Empty;
    }

    public class CreateInventoryCommandHandler(IInventoryService inventoryService, IHttpContextAccessor httpContextAccessor) 
        : IRequestHandler<CreateInventoryCommand, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IInventoryService _inventoryService = inventoryService;
        public Task<int> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            return _inventoryService.Create(request.PlayerId, cancellationToken);
        }
    }
}
