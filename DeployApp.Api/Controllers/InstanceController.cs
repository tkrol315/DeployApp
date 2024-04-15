using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeployApp.Api.Controllers
{
    [ApiController]
    [Route("deployapp/projects/{project_id}/instances")]
    public class InstanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetInstanceDto>>> GetAll(
            [FromRoute] int project_id, [FromQuery] InstanceFilterDto filter)
        {
            var query = new GetInstancesAsDtos(project_id, filter);
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
        [HttpGet("{instance_id}")]
        public async Task<ActionResult<GetInstanceDto>> Get([FromRoute] GetInstanceAsDto query)
        {
            var dto = await _mediator.Send(query);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstance([FromRoute] int project_id, [FromBody] CreateInstanceDto dto)
        {
            var command = new CreateInstance(project_id, dto);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { project_id = project_id, instance_id = id }, null);
        }
        [HttpPost("{instance_id}/tags")]
        public async Task<IActionResult> AssignTag(
            [FromRoute] int project_id, [FromRoute] int instance_id, [FromBody] AssignTagDto dto)
        {
            var command = new AssignTag(project_id, instance_id, dto);
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPost("{instance_id}/groups")]
        public async Task<IActionResult> AssignGroup(
            [FromRoute] int project_id, [FromRoute] int instance_id, [FromBody] AssignGroupDto dto)
        {
            var command = new AssignGroup(project_id,instance_id, dto);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
