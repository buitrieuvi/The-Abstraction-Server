using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.ProductVariant.Create;
using TheAbstraction.Application.Commands.ProductVariant.Delete;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.ProductVariant;

namespace TheAbstraction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductVariantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<ProductVariantResponseDTO>))]
        public async Task<IActionResult> GetProductVariantAsync()
        {
            return Ok(await _mediator.Send(new GetProductVariantQuery()));
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(List<ProductVariantResponseDTO>))]
        public async Task<IActionResult> GetProductVariantByIdAsync(string id)
        {
            return Ok(await _mediator.Send(new GetProductVariantByIdQuery() { ProductId = id }));
        }


        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> CreateProductVariant([FromBody] CreateProductVariantCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("Delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteProductVariant(string id)
        {
            var result = await _mediator.Send(new DeleteProductVariantCommand() { Id = id });
            return Ok(result);
        }
    }
}
