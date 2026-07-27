using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Product;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.Product;

namespace TheAbstraction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "admin")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        // tiếp tục hoàn thành sản phẩm và người dùng
        private readonly IMediator _mediator = mediator;

        [HttpGet("get")]
        [ProducesDefaultResponseType(typeof(List<ProductResponseDTO>))]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await _mediator.Send(new GetProductQuery());
            return Ok(result);
        }

        [HttpGet("get/{id}")]
        [ProducesDefaultResponseType(typeof(RoleResponseDTO))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery() { Id = id });
            return Ok(result);
        }

        [HttpGet("getByName/{name}")]
        [ProducesDefaultResponseType(typeof(List<ProductResponseDTO>))]
        public async Task<IActionResult> GetProductByNameAsync(string name)
        {
            var result = await _mediator.Send(new GetProductByNameQuery() { Name = name });
            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("createRange")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> CreateRange([FromBody] List<CreateProductCommand> command)
        {
            foreach (var item in command)
            {
                await _mediator.Send(item);
            }

            return Ok(1);
        }


        [HttpDelete("delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });
            return Ok(result);
        }

        [HttpPut("edit")]
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
