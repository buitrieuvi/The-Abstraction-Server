using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities;
namespace TheAbstraction.Application.Commands.Order.Create;

public class CreateOrderCommand : IRequest<int>
{
    public string UserId { get; set; }

    public List<CreateOrderDetailCommand> OrderDetails { get; set; }
}

public class CreateOrderCommandHandler(IOrderService orderService) : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderService _orderService = orderService;

    public Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        return _orderService.CreateOrderAsync(request.UserId, request.OrderDetails, cancellationToken);
    }
}
