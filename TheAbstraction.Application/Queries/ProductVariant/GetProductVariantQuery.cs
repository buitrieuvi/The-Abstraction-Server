using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.Product;

namespace TheAbstraction.Application.Queries.ProductVariant
{
    public class GetProductVariantQuery : IRequest<IReadOnlyList<ProductVariantResponseDTO>>
    {

    }

    public class GetProductVariantQueryHandler : IRequestHandler<GetProductVariantQuery, IReadOnlyList<ProductVariantResponseDTO>>
    {
        private readonly IProductVariantService _productVariantService;

        public GetProductVariantQueryHandler(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        public async Task<IReadOnlyList<ProductVariantResponseDTO>> Handle(GetProductVariantQuery request, CancellationToken cancellationToken)
        {
            return await _productVariantService.GetProductVariantAsync(cancellationToken);
        }
    }
}
