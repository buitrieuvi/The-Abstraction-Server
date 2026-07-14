using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Commands.ProductVariant.Create;
using TheAbstraction.Application.Common.Interfaces;

namespace TheAbstraction.Application.Commands.ProductVariant.Delete
{
    public class DeleteProductVariantCommand : IRequest<int>
    {
        public string Id { get; set; }
    }


    public class DeleteProductVariantCommandHandler(IProductVariantService productVariantService) : IRequestHandler<DeleteProductVariantCommand, int>
    {
        public IProductVariantService _productVariantService = productVariantService;

        public Task<int> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
        {

            return _productVariantService.DeleteProductVariantAsync(request.Id, cancellationToken);
        }
    }
}
