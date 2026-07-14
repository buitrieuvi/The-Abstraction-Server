using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using MediatR;

namespace TheAbstraction.Application.Commands.Product.Update
{
    public class UpdateProductCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateProductCommandHandler(IProductService productService) : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IProductService _productService = productService;

        public Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return _productService.UpdateAsync(request.Id, request.Name, request.Description, request.IsActive, cancellationToken);
        }
    }
}