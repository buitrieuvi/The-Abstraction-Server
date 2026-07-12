using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.Product
{
    public class GetProductQuery : IRequest<IReadOnlyList<ProductResponseDTO>>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductQuery, IReadOnlyList<ProductResponseDTO>>
    {
        private readonly IProductService _productService;

        public GetProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IReadOnlyList<ProductResponseDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsync(cancellationToken);
        }
    }
}
