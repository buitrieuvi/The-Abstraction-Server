using Microsoft.AspNetCore.Mvc;
using MediatR;
using TheAbstraction.Application.Commands.Order.Create;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TheAbstraction.Application.Commands.Order.Update;

namespace TheAbstraction.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("Create")]
    [ProducesDefaultResponseType(typeof(int))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<int>> Create([FromBody] CreateOrderCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        command.UserId = userId;
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("Edit")]
    [ProducesDefaultResponseType(typeof(int))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<int>> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        command.UserId = userId;
        
        return Ok(await _mediator.Send(command));
    }

}

