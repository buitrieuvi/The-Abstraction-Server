using TheAbstraction.Application.Common.Interfaces;
using MediatR;

namespace TheAbstraction.Application.Commands.Product.Delete
{
    public class DeleteProductCommand : IRequest<int>
    {
        public string Id { get; set; } 
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
