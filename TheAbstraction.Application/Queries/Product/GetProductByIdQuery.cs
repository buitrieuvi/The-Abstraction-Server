using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using MediatR;

namespace TheAbstraction.Application.Queries.Product
{
    public class GetProductByIdQuery : IRequest<ProductResponseDTO>
    {
        public string Id { get; set; }
    }

    public class GetProductByIdQueryHandler(IProductService productService) : IRequestHandler<GetProductByIdQuery, ProductResponseDTO>
    {
        private readonly IProductService _productService = productService;

        public Task<ProductResponseDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
