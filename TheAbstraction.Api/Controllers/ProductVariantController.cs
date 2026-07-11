using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Product.Create;
using TheAbstraction.Application.Commands.ProductVariant.Create;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.Product;

namespace TheAbstraction.Api.Controllers
{
    public class ProductVariantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductVariantController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductVariantCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
