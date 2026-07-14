using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.Product
{
    public class GetProductByNameQuery : IRequest<IReadOnlyList<ProductResponseDTO>>
    {
        public string Name { get; set; }
    }

    public class GetProductByNameQueryHandler(IProductService productService) : IRequestHandler<GetProductByNameQuery, IReadOnlyList<ProductResponseDTO>>
    {
        private readonly IProductService _productService = productService;

        public Task<IReadOnlyList<ProductResponseDTO>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetByNameAsync(request.Name, cancellationToken);
        }
    }
}
