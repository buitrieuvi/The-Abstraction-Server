using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.Product
{
    public class GetProductQuery : IRequest<IReadOnlyList<ProductResponseDTO>>;

    public class GetProductsQueryHandler(IProductService productService) : IRequestHandler<GetProductQuery, IReadOnlyList<ProductResponseDTO>>
    {
        private readonly IProductService _productService = productService;

        public Task<IReadOnlyList<ProductResponseDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetAllAsync(cancellationToken);
        }
    }
}
