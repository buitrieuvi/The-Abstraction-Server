using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities;

namespace TheAbstraction.Application.Commands.Order.Update;

public class UpdateOrderStatusCommand : IRequest<int>
{
    public string OrderId { get; set; }
    public string UserId { get; set; }
    public OrderStatus NewStatus { get; set; }
}

public class UpdateOrderStatusCommandHandler(IOrderService orderService) : IRequestHandler<UpdateOrderStatusCommand, int>
{
    private readonly IOrderService _orderService = orderService;

    public Task<int> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        return _orderService.UpdateOrderStatus(request.OrderId, request.UserId, request.NewStatus, cancellationToken);
    }
}