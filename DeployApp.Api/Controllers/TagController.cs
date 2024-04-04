using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Queries;
using FluentValidation;
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
        [HttpGet("{id}")]
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
        public async Task<IActionResult> CreateTag([FromBody] CreateTag command, IValidator<CreateTag> validator)
        {
            var result = await validator.ValidateAsync(command);
            if(!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get),new { id =id},null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTag([FromRoute] RemoveTag command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag([FromRoute] int id, [FromBody] UpdateTagDto dto, IValidator<UpdateTag> validator)
        {
            var command = new UpdateTag(id, dto);
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await _mediator.Send(command);
            return Ok();
        }
    }
}
