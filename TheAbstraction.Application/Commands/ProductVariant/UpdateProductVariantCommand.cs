#nullable enable
using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace TheAbstraction.Application.Commands.ProductVariant;

public class UpdateProductVariantCommand : IRequest<int>
{
    public string Id { get; set; } = string.Empty;
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
    [StringLength(50)] public string? Model { get; set; }
    [StringLength(50)] public string? Color { get; set; }
    [StringLength(20)] public string? Size { get; set; }
}

public class UpdateProductVariantCommandHandler(IProductVariantService productVariantService) : IRequestHandler<UpdateProductVariantCommand, int>
{
    private readonly IProductVariantService _productVariantService = productVariantService;

    public async Task<int> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        return await _productVariantService.UpdateProductVariantAsync(request.Id, request.Price, request.Quantity, request.Model, request.Color, request.Size, cancellationToken);
    }
}
