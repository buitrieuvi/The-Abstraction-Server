using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities;

namespace TheAbstraction.Application.Commands.Order.Create;


public class CreateOrderCommand : IRequest<int>
{
    public List<CreateOrderDetailCommand> OrderDetails { get; set; }
}

public class CreateOrderCommandHandler(IOrderService orderService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderService _orderService = orderService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        var userId = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return _orderService.CreateOrderAsync(userId, request.OrderDetails, cancellationToken);
    }
}
