using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Item;
using TheAbstraction.Application.Queries.Item;

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

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetItemsQuery());
            return Ok(result);
        }

        [HttpDelete("delete/{itemId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> Delete(string itemId)
        {
            var result = await _mediator.Send(new DeleteItemCommand() { ItemId = itemId});
            return Ok(result);
        }
    }
}
