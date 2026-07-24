using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
            return Ok(await _mediator.Send(new GetProductQuery()));
        }

        [HttpGet("get/{id}")]
        [ProducesDefaultResponseType(typeof(RoleResponseDTO))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery() { Id = id }));
        }

        [HttpGet("getByName/{name}")]
        [ProducesDefaultResponseType(typeof(List<ProductResponseDTO>))]
        public async Task<IActionResult> GetProductByNameAsync(string name)
        {
            return Ok(await _mediator.Send(new GetProductByNameQuery() { Name = name }));
        }

        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
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


        [HttpDelete("delete")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteProductAsync()
        {
            return Ok(await _mediator.Send(new DeleteProductCommand()));
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
