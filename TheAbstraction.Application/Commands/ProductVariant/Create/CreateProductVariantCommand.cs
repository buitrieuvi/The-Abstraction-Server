using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.ProductVariant.Create
{
    public class CreateProductVariantCommand : IRequest<int>
    {
        public string ProductId { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, int>
    {
        public IProductVariantService _productVariantService;

        public CreateProductVariantCommandHandler(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }
        public async Task<int> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            return await _productVariantService.CreateAsync(request.ProductId, request.Price, request.Quantity);
        }
    }
}
