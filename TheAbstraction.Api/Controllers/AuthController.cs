using TheAbstraction.Application.Commands.Auth;
using TheAbstraction.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TheAbstraction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
