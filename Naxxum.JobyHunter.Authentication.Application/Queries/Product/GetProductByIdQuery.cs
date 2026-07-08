using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;

namespace Authentication.Application.Queries.Product
{
    public class GetProductByIdQuery : IRequest<ProductResponseDTO?>
    {
        public string Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDTO?>
    {
        private readonly IProductService _productService;

        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductResponseDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
