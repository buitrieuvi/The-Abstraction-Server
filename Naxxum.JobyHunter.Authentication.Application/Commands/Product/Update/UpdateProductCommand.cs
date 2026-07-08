using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;

namespace Authentication.Application.Commands.Product.Update
{
    public class UpdateProductCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateAsync(request.Id, request.Name, request.Description, request.Price, request.StockQuantity, request.IsActive, cancellationToken);
            return result ? 1 : 0;
        }
    }
}
