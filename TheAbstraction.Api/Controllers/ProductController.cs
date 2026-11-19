using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Product;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.Product;

namespace TheAbstraction.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "admin")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponseDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductResponseDTO>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetProductQuery(), cancellationToken));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponseDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponseDTO>> GetById(string id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetProductByIdQuery { Id = id }, cancellationToken));

    [HttpGet("search")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponseDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductResponseDTO>>> Search([FromQuery] string name, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetProductByNameQuery { Name = name }, cancellationToken));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var affectedRows = await mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, new { affectedRows });
    }

    [HttpPost("batch")]
    public async Task<IActionResult> CreateBatch([FromBody] List<CreateProductCommand> commands, CancellationToken cancellationToken)
    {
        foreach (var command in commands)
            await mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteProductCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}
