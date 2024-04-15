using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeployApp.Api.Controllers
{
    [ApiController]
    [Route("deployapp/tags")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{tag_id}")]
        public async Task<ActionResult<GetTagDto>> Get([FromRoute] GetTagAsDto query)
        {
            var tag = await _mediator.Send(query);
            return Ok(tag);
        }
        [HttpGet]
        public async Task<ActionResult<List<GetTagDto>>> GetAll([FromQuery]GetTagsAsDtos query)
        {
            var tags = await _mediator.Send(query);
            return Ok(tags);
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTag command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get),new { tag_id =id},null);
        }

        [HttpDelete("{tag_id}")]
        public async Task<IActionResult> RemoveTag([FromRoute] RemoveTag command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut("{tag_id}")]
        public async Task<IActionResult> UpdateTag([FromRoute] int tag_id, [FromBody] UpdateTagDto dto)
        {
            var command = new UpdateTag(tag_id, dto);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
