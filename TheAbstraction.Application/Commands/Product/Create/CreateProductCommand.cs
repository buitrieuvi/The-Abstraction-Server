using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using MediatR;
using TheAbstraction.Application.Commands.ProductVariant.Create;

namespace TheAbstraction.Application.Commands.Product.Create;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;

    public List<CreateProductVariantCommand> ProductVariant { get; set; }
}

public class CreateProductCommandHandler(IProductService productService) : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductService _productService = productService;

    public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return _productService.CreateAsync(request.Name, request.Description, request.IsActive, request.ProductVariant, cancellationToken);
    }
}
