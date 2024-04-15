using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeployApp.Api.Controllers
{
    [ApiController]
    [Route("deployapp/groups")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{group_id}")]

        public async Task<ActionResult<GetGroupDto>> Get([FromRoute] GetGroupAsDto query)
        {
            var group = await _mediator.Send(query);
            return Ok(group);
        }
        [HttpGet]
        public async Task<ActionResult<List<GetGroupDto>>> GetAll([FromQuery] GetGroupsAsDtos query)
        {
            var groups = await _mediator.Send(query);
            return Ok(groups);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroup command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { group_id = id }, null);
        }
        [HttpPut("{group_id}")]
        public async Task<IActionResult> UpdateGroup([FromRoute] int group_id, [FromBody] UpdateGroupDto dto)
        {
            var command = new UpdateGroup(group_id, dto);
            await _mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{group_id}")]
        public async Task<IActionResult> RemoveGroup([FromRoute] RemoveGroup command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
