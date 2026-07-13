using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using MediatR;
using TheAbstraction.Application.Commands.ProductVariant.Create;

namespace TheAbstraction.Application.Commands.Product.Create
{

    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;

        public List<CreateProductVariantCommand> ProductVariant { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            return await _productService.CreateAsync(request.Name, request.Description, request.StockQuantity, request.IsActive, request.ProductVariant, cancellationToken);
        }
    }
}
