using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Auth;
using TheAbstraction.Application.DTOs.Auth;

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
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
