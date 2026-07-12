using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Product.Create;
using TheAbstraction.Application.Commands.Product.Delete;
using TheAbstraction.Application.Commands.Product.Update;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.Product;

namespace TheAbstraction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<ProductResponseDTO>))]
        public async Task<IActionResult> GetProductAsync()
        {
            return Ok(await _mediator.Send(new GetProductQuery()));
        }

        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(RoleResponseDTO))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery() { Id = id }));
        }

        [HttpGet("GetByName/{name}")]
        [ProducesDefaultResponseType(typeof(List<ProductResponseDTO>))]
        public async Task<IActionResult> GetProductByNameAsync(string name)
        {
            return Ok(await _mediator.Send(new GetProductByNameQuery() { Name = name }));
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpDelete("Delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand()
            {
                Id = id
            }));
        }

        [HttpPut("Edit/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditProduct(string id, [FromBody] UpdateProductCommand command)
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
