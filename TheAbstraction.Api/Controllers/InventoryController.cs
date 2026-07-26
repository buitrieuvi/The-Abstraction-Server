using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheAbstraction.Application.Commands.Inventory;
using TheAbstraction.Application.Queries.Inventory;

namespace TheAbstraction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> Create([FromBody] CreateInventoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetInventoryQuery());
            return Ok(result);
        }

        [HttpPut("{playerId}/items/{itemId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult<int>> UpdateItem(string playerId, string itemId, [FromBody] int quantity)
        {
            var result = await _mediator.Send(new UpdateInventoryItemCommand
            {
                PlayerId = playerId,
                ItemId = itemId,
                Quantity = quantity
            });
            return Ok(result);
        }
    }
}
