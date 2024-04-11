using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeployApp.Api.Controllers
{
    [ApiController]
    [Route("deployapp/projects{project_id}/instances")]
    public class InstanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetInstanceDto>>> GetAll([FromRoute] int project_id, [FromQuery] InstanceFilterDto searchPhrase) 
        {
            var query = new GetInstancesAsDtos(project_id, searchPhrase);
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
        [HttpGet("{instance_id}")]
        public async Task<ActionResult<GetInstanceDto>> Get([FromRoute] int project_id, [FromRoute] int instance_id) 
        { 
            var query = new GetInstanceAsDto(project_id, instance_id);
            var dto = await _mediator.Send(query);
            return Ok(dto);
        } 

        [HttpPost]
        public async Task<IActionResult> CreateInstance([FromRoute] int project_id, [FromBody] CreateInstanceDto dto, IValidator<CreateInstance> validator)
        {
            var command = new CreateInstance(project_id, dto);
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var id = await _mediator.Send(command);
            return Created();
        }
        [HttpPost("{instance_id}/tags/{tag_id}")]
        public async Task<IActionResult> AssignTag([FromRoute] int project_id, [FromRoute] int instance_id, [FromRoute] int tag_id)
        {
            var command = new AssignTagToInstance(project_id, instance_id, tag_id);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
