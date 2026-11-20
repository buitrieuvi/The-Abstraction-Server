using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Role;
using TheAbstraction.Application.DTOs.Role;
using TheAbstraction.Application.Queries.Role;

namespace TheAbstraction.Api.Controllers;

[Route("api/v1/roles")]
[ApiController]
public class RoleController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateCommand command)
    {
        var result = await mediator.Send(command);
        return result > 0 ? StatusCode(StatusCodes.Status201Created) : BadRequest();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<RoleResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await mediator.Send(new GetRoleQuery()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RoleResponseDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleById(string id)
    {
        return Ok(await mediator.Send(new GetRoleByIdQuery { RoleId = id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleCommand command)
    {
        command.RoleId = id;
        var result = await mediator.Send(command);
        return result > 0 ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var result = await mediator.Send(new DeleteRoleCommand { RoleId = id });
        return result ? NoContent() : NotFound();
    }
}
