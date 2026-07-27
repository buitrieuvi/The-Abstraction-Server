using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Order;
using TheAbstraction.Domain.Entities;

namespace TheAbstraction.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// [Authorize(Roles = "user , admin")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("create")]
    [ProducesDefaultResponseType(typeof(int))]
    public async Task<ActionResult<int>> Create([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("editStatus/{orderId}")]
    [ProducesDefaultResponseType(typeof(int))]
    public async Task<ActionResult<int>> UpdateOrderStatus(string orderId, [FromBody] string newStatus)
    {
        var result = await _mediator.Send(new UpdateOrderStatusCommand()
        {
            OrderId = orderId,
            NewStatus = newStatus
        });
        return Ok(result);
    }
}
