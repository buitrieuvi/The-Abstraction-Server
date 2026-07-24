using TheAbstraction.Application.Common.Interfaces;
using MediatR;
using TheAbstraction.Application.Common.Exceptions;

namespace TheAbstraction.Application.Commands.Product.Delete
{
    public class DeleteProductCommand : IRequest<int>
    {
    }

    public class DeleteProductCommandHandler(IProductService productService) : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductService _productService = productService;

        public Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return _productService.DeleteAsync(cancellationToken);
        }
    }
}
