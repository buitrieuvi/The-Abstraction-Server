#nullable enable
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using MediatR;
using TheAbstraction.Application.Commands.ProductVariant;
using System.ComponentModel.DataAnnotations;

namespace TheAbstraction.Application.Commands.Product;

public class CreateProductCommand : IRequest<int>
{
    [Required, StringLength(200, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    [StringLength(1000)]
    public string? Description { get; set; }

    public List<CreateProductVariantCommand> ProductVariant { get; set; } = [];
}

public class CreateProductCommandHandler(IProductService productService) : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductService _productService = productService;

    public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return _productService.CreateAsync(request.Name, request.Description, request.ProductVariant, cancellationToken);
    }
}
