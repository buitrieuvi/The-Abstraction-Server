using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Queries.ProductVariant
{
    public class GetPriceProductVariantByIdQuery : IRequest<decimal>
    {
        public string Id { get; set; }
    }

    public class GetPriceProductVariantByIdQueryHandler(IProductVariantService productVariantService) : IRequestHandler<GetPriceProductVariantByIdQuery, decimal>
    {
        private readonly IProductVariantService _productVariantService = productVariantService;

        public Task<decimal> Handle(GetPriceProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
            return _productVariantService.GetPriceProductVariantById(request.Id, cancellationToken);
        }
    }

}
