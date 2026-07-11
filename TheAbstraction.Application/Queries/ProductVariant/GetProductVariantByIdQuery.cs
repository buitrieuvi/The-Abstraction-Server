using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.ProductVariant
{
    public class GetProductVariantByIdQuery : IRequest<IReadOnlyList<ProductVariantResponseDTO>>
    {
        public string ProductId { get; set; }
    }

    public class GetProductVariantByIdQueryHandler : IRequestHandler<GetProductVariantByIdQuery, IReadOnlyList<ProductVariantResponseDTO>>
    {
        private readonly IProductVariantService _productVariantService;

        public GetProductVariantByIdQueryHandler(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        public async Task<IReadOnlyList<ProductVariantResponseDTO>> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productVariantService.GetProductVariantByProductIdAsync(request.ProductId, cancellationToken);
        }
    }

}
