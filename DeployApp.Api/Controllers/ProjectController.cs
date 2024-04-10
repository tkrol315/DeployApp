using DeployApp.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTagDto>> Get([FromRoute] GetProjectAsDto query)
        {
            var project = await _mediator.Send(query);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProject command, IValidator<CreateProject> validator)
        {
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProject([FromRoute] RemoveProject command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] UpdateProjectDto dto, IValidator<UpdateProject> validator)
        {
            var command = new UpdateProject(id, dto);   
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
