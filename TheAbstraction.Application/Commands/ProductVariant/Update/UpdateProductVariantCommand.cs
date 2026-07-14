using MediatR;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.ProductVariant.Update;

public class UpdateProductVariantCommand : IRequest<int>
{
    public string Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
}

public class UpdateProductVariantCommandHandler(IProductVariantService productVariantService) : IRequestHandler<UpdateProductVariantCommand, int>
{
    private readonly IProductVariantService _productVariantService = productVariantService;

    public async Task<int> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        return await _productVariantService.UpdateProductVariantAsync(request.Id, request.Price, request.Quantity, request.IsActive,
         request.Model, request.Color, request.Size, cancellationToken);
    }
}
