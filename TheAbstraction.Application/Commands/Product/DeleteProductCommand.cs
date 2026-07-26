using TheAbstraction.Application.Common.Interfaces;
using MediatR;
using TheAbstraction.Application.Common.Exceptions;

namespace TheAbstraction.Application.Commands.Product
{
    public class DeleteProductCommand : IRequest<int>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class DeleteProductCommandHandler(IProductService productService) : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductService _productService = productService;

        public Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return _productService.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
