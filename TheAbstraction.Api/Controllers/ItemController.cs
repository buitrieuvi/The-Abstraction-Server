using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.InventoryItem.Create;

namespace TheAbstraction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "admin")]
    public class ItemController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> Create([FromBody] CreateItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[HttpDelete("Delete/{playerId}")]
        //[ProducesDefaultResponseType(typeof(int))]
        //public async Task<IActionResult> Delete(string playerId)
        //{
        //    return Ok(await _mediator.Send(new DeletePlayerCommand() { PlayerId = playerId }));
        //}
    }
}
