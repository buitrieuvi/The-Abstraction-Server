using MediatR;

namespace TheAbstraction.Application.Commands.Order.Create;

public class CreateOrderDetailCommand : IRequest<int>
{
    public string ProductVariantId { get; set; }
    public int Quantity { get; set; }
}
