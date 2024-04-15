using DeployApp.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeployApp.Application.Commands;
using DeployApp.Application.Queries;

namespace DeployApp.Api.Controllers
{
    [ApiController]
    [Route("deployapp/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetTagDto>>> GetAll([FromRoute] GetProjectsAsDtos command)
        {
            var projects = await _mediator.Send(command);
            return Ok(projects);
        }

        [HttpGet("{project_id}")]
        public async Task<ActionResult<GetTagDto>> Get([FromRoute] GetProjectAsDto query)
        {
            var project = await _mediator.Send(query);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProject command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { project_id = id }, null);
        }

        [HttpDelete("{project_id}")]
        public async Task<IActionResult> RemoveProject([FromRoute] RemoveProject command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{project_id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int project_id, [FromBody] UpdateProjectDto dto)
        {
            var command = new UpdateProject(project_id, dto);   
            await _mediator.Send(command);
            return Ok();
        }

    }
}
