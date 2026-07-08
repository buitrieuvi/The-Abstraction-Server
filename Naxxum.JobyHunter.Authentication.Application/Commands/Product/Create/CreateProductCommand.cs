using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;

namespace Authentication.Application.Commands.Product.Create
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
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
            return await _productService.CreateAsync(request.Name, request.Description, request.Price, request.StockQuantity, request.IsActive, cancellationToken);
        }
    }
}
