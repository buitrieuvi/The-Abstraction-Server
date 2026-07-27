using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.ProductVariant;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.ProductVariant;

namespace TheAbstraction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("Get")]
        [ProducesDefaultResponseType(typeof(List<ProductVariantResponseDTO>))]
        public async Task<IActionResult> GetProductVariantAsync()
        {
            var result = await _mediator.Send(new GetProductVariantQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(List<ProductVariantResponseDTO>))]
        public async Task<IActionResult> GetProductVariantByIdAsync(string id)
        {
            var result = await _mediator.Send(new GetProductVariantQuery());
            return Ok(result);
        }

        [HttpGet("GetPrice/{id}")]
        [ProducesDefaultResponseType(typeof(List<ProductVariantResponseDTO>))]
        public async Task<IActionResult> GetPriceProductVariantById(string id)
        {
            var result = await _mediator.Send(new GetPriceProductVariantByIdQuery() { Id = id });
            return Ok(result);
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> CreateProductVariant([FromBody] CreateProductVariantCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteProductVariant(string id)
        {
            var result = await _mediator.Send(new DeleteProductVariantCommand() { Id = id });
            return Ok(result);
        }

        [HttpPut("Edit/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditProductVariant(string id, [FromBody] UpdateProductVariantCommand command)
        {

            if (id == command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
