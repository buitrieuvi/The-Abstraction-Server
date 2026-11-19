using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.ProductVariant;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.ProductVariant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TheAbstraction.Api.Controllers;

[Route("api/products/variants")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "admin")]
public class ProductVariantController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductVariantResponseDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductVariantResponseDTO>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetProductVariantQuery(), cancellationToken));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductVariantResponseDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductVariantResponseDTO>> GetById(string id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetProductVariantByIdQuery { Id = id }, cancellationToken));

    [HttpGet("by-product/{productId}")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductVariantResponseDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductVariantResponseDTO>>> GetByProduct(string productId, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetProductVariantsByProductIdQuery { ProductId = productId }, cancellationToken));

    [HttpGet("{id}/price")]
    public async Task<ActionResult<decimal>> GetPrice(string id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetPriceProductVariantByIdQuery { Id = id }, cancellationToken));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateProductVariantCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProductVariantCommand command, CancellationToken cancellationToken)
    {
        command.Id = id;
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteProductVariantCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}
