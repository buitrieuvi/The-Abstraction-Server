using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.User;
using TheAbstraction.Application.DTOs.User;
using TheAbstraction.Application.Queries.User;

namespace TheAbstraction.Api.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetUserDetails), new { id = result }, new { id = result });
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<UserDetailsResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers([FromQuery] string? userName)
    {
        if (!string.IsNullOrWhiteSpace(userName))
        {
            var user = await mediator.Send(new GetUserDetailsByUserNameQuery { UserName = userName });
            return Ok(user);
        }

        return Ok(await mediator.Send(new GetAllUsersDetailsQuery()));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDetailsResponseDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserDetails(string id)
    {
        var result = await mediator.Send(new GetUserDetailsQuery { UserId = id });
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] EditUserProfileCommand command)
    {
        command.Id = id;
        var result = await mediator.Send(command);
        return result > 0 ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await mediator.Send(new DeleteUserCommand { UserId = id });
        return result > 0 ? NoContent() : NotFound();
    }

    [HttpPut("{id}/roles")]
    public async Task<IActionResult> UpdateUserRoles(string id, [FromBody] UpdateUserRolesCommand command)
    {
        command.UserId = id;
        var result = await mediator.Send(command);
        return result > 0 ? NoContent() : NotFound();
    }
}
