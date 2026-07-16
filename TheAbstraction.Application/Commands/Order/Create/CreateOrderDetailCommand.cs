using MediatR;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.Order.Create;

public class CreateOrderDetailCommand : IRequest<int>
{
    public string ProductVariantId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderDetailCommandHandler(IOrderDetailService orderDetailService) : IRequestHandler<CreateOrderDetailCommand, int>
{
    private readonly IOrderDetailService _orderDetailService = orderDetailService;
    public Task<int> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        return null;
        //return _orderDetailService.CreateOrderDetailAsync(request.OrderId, request.ProductVariantId, request.Quantity, cancellationToken);
    }
}