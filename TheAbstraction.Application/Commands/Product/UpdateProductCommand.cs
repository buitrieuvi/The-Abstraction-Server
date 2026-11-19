#nullable enable
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace TheAbstraction.Application.Commands.Product
{
    public class UpdateProductCommand : IRequest<int>
    {
        public string Id { get; set; } = string.Empty;
        [Required, StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        [StringLength(1000)]
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
    }

    public class UpdateProductCommandHandler(IProductService productService) : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IProductService _productService = productService;

        public Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return _productService.UpdateAsync(request.Id, request.Name, request.Description, cancellationToken);
        }
    }
}
