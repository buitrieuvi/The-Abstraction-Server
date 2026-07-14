using Microsoft.AspNetCore.Mvc;
using MediatR;
using TheAbstraction.Application.Commands.Order.Create;

namespace TheAbstraction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("Create")]
    [ProducesDefaultResponseType(typeof(int))]
    public async Task<ActionResult<int>> Create([FromBody] CreateOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

}
