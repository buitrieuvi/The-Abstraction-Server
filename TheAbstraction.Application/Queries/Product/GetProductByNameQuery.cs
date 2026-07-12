using MediatR;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.Product
{
    public class GetProductByNameQuery : IRequest<IReadOnlyList<ProductResponseDTO>>
    {
        public string Name { get; set; } 
    }

    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IReadOnlyList<ProductResponseDTO>>
    {
        private readonly IProductService _productService;

        public GetProductByNameQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IReadOnlyList<ProductResponseDTO>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetByNameAsync(request.Name, cancellationToken);
        }
    }
}
