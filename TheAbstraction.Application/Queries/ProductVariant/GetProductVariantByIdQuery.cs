using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.ProductVariant
{
    public class GetProductVariantByIdQuery : IRequest<ProductVariantResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class GetProductVariantByIdQueryHandler(IProductVariantService productVariantService) : IRequestHandler<GetProductVariantByIdQuery, ProductVariantResponseDTO>
    {
        private readonly IProductVariantService _productVariantService = productVariantService;

        public Task<ProductVariantResponseDTO> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
            return _productVariantService.GetProductVariantById(request.Id, cancellationToken);
        }
    }

}
