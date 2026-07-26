using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities;

// namespace TheAbstraction.Application.Commands.Order;
namespace TheAbstraction.Application.Commands.Order;

public class UpdateOrderStatusCommand : IRequest<int>
{
    public string OrderId { get; set; }
    public string NewStatus { get; set; }
}

public class UpdateOrderStatusCommandHandler(IOrderService orderService) : IRequestHandler<UpdateOrderStatusCommand, int>
{
    private readonly IOrderService _orderService = orderService;

    public Task<int> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        return _orderService.UpdateOrderStatus(request.OrderId, request.NewStatus, cancellationToken);
    }
}