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
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

    }

    public class CreateProductVariantCommandHandler(IProductVariantService productVariantService) : IRequestHandler<CreateProductVariantCommand, int>
    {
        public IProductVariantService _productVariantService = productVariantService;

        public Task<int> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            return _productVariantService.CreateProductVariantAsync(
                request.Price,
                request.Quantity,
                request.Model,
                request.Color,
                request.Size,
                cancellationToken);
        }
    }
}
