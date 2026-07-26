using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TheAbstraction.Application.Commands.Player;
using TheAbstraction.Application.Common.Exceptions;

namespace TheAbstraction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<int>> Create([FromBody] CreatePlayerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeletePlayerByUserId/{playerId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> Delete(string playerId)
        {
            var result = await _mediator.Send(new DeletePlayerCommand() { PlayerId = playerId });
            return Ok(result);
        }

        
    }
}
