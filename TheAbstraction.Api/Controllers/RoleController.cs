using TheAbstraction.Application.Commands.Role.Create;
using TheAbstraction.Application.Commands.Role.Update;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.Role;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Role.Delete;


namespace TheAbstraction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Admin, Management")]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        public readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> CreateRoleAsync([FromBody] RoleCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("get")]
        [ProducesDefaultResponseType(typeof(List<RoleResponseDTO>))]
        public async Task<IActionResult> GetRoleAsync()
        {
            var result = await _mediator.Send(new GetRoleQuery());
            return Ok(result);
        }


        [HttpGet("get/{id}")]
        [ProducesDefaultResponseType(typeof(RoleResponseDTO))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {

            var result = await _mediator.Send(new GetRoleByIdQuery() { RoleId = id });
            return Ok(result);
        }

        [HttpDelete("delete")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteRoleAsync([FromBody] DeleteRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("edit")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditRole([FromBody] UpdateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
