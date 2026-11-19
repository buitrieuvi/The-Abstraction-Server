using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.ProductVariant;

public sealed class GetProductVariantsByProductIdQuery : IRequest<IReadOnlyList<ProductVariantResponseDTO>>
{
    public string ProductId { get; set; } = string.Empty;
}

public sealed class GetProductVariantsByProductIdQueryHandler(IProductVariantService productVariantService)
    : IRequestHandler<GetProductVariantsByProductIdQuery, IReadOnlyList<ProductVariantResponseDTO>>
{
    public Task<IReadOnlyList<ProductVariantResponseDTO>> Handle(GetProductVariantsByProductIdQuery request, CancellationToken cancellationToken) =>
        productVariantService.GetProductVariantByProductIdAsync(request.ProductId, cancellationToken);
}
