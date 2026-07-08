using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;

namespace Authentication.Application.Queries.Product
{
    public class GetProductQuery : IRequest<IReadOnlyList<ProductResponseDTO>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetProductQuery, IReadOnlyList<ProductResponseDTO>>
    {
        private readonly IProductService _productService;

        public GetAllProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IReadOnlyList<ProductResponseDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsync(cancellationToken);
        }
    }
}
