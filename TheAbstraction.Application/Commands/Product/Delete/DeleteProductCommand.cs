using TheAbstraction.Application.Common.Interfaces;
using MediatR;
using TheAbstraction.Application.Common.Exceptions;

namespace TheAbstraction.Application.Commands.Product.Delete
{
    public class DeleteProductCommand : IRequest<int>
    {
        public string Id { get; set; }
    }

    public class DeleteProductCommandHandler(IProductService productService) : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductService _productService = productService;

        public Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productId = _productService.GetByIdAsync(request.Id).Result.Id;
            if (productId == null) { throw new NotFoundException("Không tìm thấy người dùng"); }
            return _productService.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
