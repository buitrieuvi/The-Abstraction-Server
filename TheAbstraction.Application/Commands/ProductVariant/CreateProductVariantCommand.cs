#nullable enable
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace TheAbstraction.Application.Commands.ProductVariant
{
    public class CreateProductVariantCommand : IRequest<int>
    {
        public string ProductId { get; set; } = string.Empty;
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [StringLength(50)] public string? Model { get; set; }
        [StringLength(50)] public string? Color { get; set; }
        [StringLength(20)] public string? Size { get; set; }

    }

    public class CreateProductVariantCommandHandler(IProductVariantService productVariantService) : IRequestHandler<CreateProductVariantCommand, int>
    {
        public IProductVariantService _productVariantService = productVariantService;

        public Task<int> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
        {
            return _productVariantService.CreateProductVariantAsync(
                request.ProductId,
                request.Price,
                request.Quantity,
                request.Model,
                request.Color,
                request.Size,
                cancellationToken);
        }
    }
}
