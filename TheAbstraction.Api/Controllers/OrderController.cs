using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Order.Create;
using TheAbstraction.Application.Commands.Order.Update;
namespace TheAbstraction.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "user , admin")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("create")]
    [ProducesDefaultResponseType(typeof(int))]
    public async Task<ActionResult<int>> Create([FromBody] CreateOrderCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [HttpPost("edit")]
    [ProducesDefaultResponseType(typeof(int))]
    public async Task<ActionResult<int>> UpdateOrderStatus([FromBody] UpdateOrderStatusCommand command)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        command.UserId = userId;
        return Ok(await _mediator.Send(command));
    }
}
