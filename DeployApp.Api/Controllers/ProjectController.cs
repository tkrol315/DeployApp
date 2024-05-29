using DeployApp.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeployApp.Application.Commands;
using DeployApp.Application.Queries;
using DeployApp.Domain.Enums;

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
        public async Task<ActionResult<List<GetTagDto>>> GetAll([FromQuery] ProjectFilterDto filter)
        {
            var query = new GetProjectsAsDtos(filter);
            var projects = await _mediator.Send(query);
            return Ok(projects);
        }

        [HttpGet("{project_id}")]
        public async Task<ActionResult<GetTagDto>> Get([FromRoute] GetProjectAsDto query)
        {
            var project = await _mediator.Send(query);
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var command = new CreateProject(dto);
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

        [HttpPost("{project_id}/versions")]
        public async Task<IActionResult> CreateProjectVersionAction([FromRoute] int project_id, [FromBody] CreateProjectVersionDto dto)
        {
            var command = new CreateProjectVersion(project_id, dto);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProjectVersionAction), new { project_id = project_id, version_id = id }, null);
        }
        [HttpGet("{project_id}/versions/{version_id}")]
        public async Task<ActionResult<GetProjectVersionDto>> GetProjectVersionAction([FromRoute] GetProjectVersionAsDto query)
        {
            var version = await _mediator.Send(query);
            return Ok(version);
        }

        [HttpGet("{project_id}/versions")]
        public async Task<ActionResult<List<GetProjectVersionDto>>> GetAllProjectVersionsAction([FromRoute] GetProjectVersionsAsDtos command)
        {
            var versions = await _mediator.Send(command);
            return Ok(versions);
        }

        [HttpPut("{project_id}/versions/{version_id}")]
        public async Task<IActionResult> UpdateProjectVersionAction([FromRoute] int project_id, [FromRoute] int version_id, [FromBody] UpdateProjectVersionDto dto)
        {
            var command = new UpdateProjectVersion(project_id, version_id, dto);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{project_id}/versions/{version_id}")]
        public async Task<IActionResult> RemoveProjectVersionAction([FromRoute] RemoveProjectVersion command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{project_id}/deploys")]
        public async Task<ActionResult<List<GetDeployDto>>> GetProjectDeploys([FromRoute] GetDeploysAsDtos query)
        {
            var deploys = await _mediator.Send(query);
            return Ok(deploys);
        }

        [HttpPost("{project_id}/deploys")]
        public async Task<IActionResult> CreateProjectDeploy([FromRoute] int project_id, [FromBody] CreateDeployDto dto)
        {
            var command = new CreateDeploy(project_id, dto);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProjectDeploy), new { project_id = project_id, deploy_id = id}, null);
        }

        [HttpGet("{project_id}/deploys/{deploy_id}")]
        public async Task<ActionResult<GetDeployDto>> GetProjectDeploy([FromRoute] GetDeployAsDto query)
        {
            var deploy = await _mediator.Send(query);
            return Ok(deploy);
        }

        [HttpDelete("{project_id}/deploys/{deploy_id}")]
        public async Task<IActionResult> RemoveProjectDeploy([FromRoute] RemoveDeploy command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpPut("{project_id}/deploys/{deploy_id}")]
        public async Task<IActionResult> UpdateProjectDeploy([FromRoute] int project_id, [FromRoute] int deploy_id, [FromBody] UpdateDeployDto dto)
        {
            var command = new UpdateDeploy(project_id, deploy_id, dto);
            await _mediator.Send(command);
            return Ok();
        }
        [HttpGet("{project_id}/deploys/{deploy_id}/instances")]
        public async Task<ActionResult<List<GetInstanceDto>>> GetDeployInstances([FromRoute] int project_id, [FromRoute] int deploy_id, [FromQuery] Status? status)
        {
            var query = new GetDeployInstancesAsDtos(project_id, deploy_id, status);
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
        [HttpGet("{project_id}/deploys/{deploy_id}/instances/{instance_id}")]
        public async Task<ActionResult<GetInstanceDto>> GetDeployInstance([FromRoute] GetDeployInstanceAsDto query)
        {
            var dto = await _mediator.Send(query);
            return Ok(dto);
        }
        [HttpPost("{project_id}/deploys/{deploy_id}/instances")]
        public async Task<IActionResult> AssignInstanceToDeploy(
            [FromRoute] int project_id, [FromRoute] int deploy_id, [FromBody] AssignInstanceToDeployDto dto)
        {
            var command = new AssignInstanceToDeploy(project_id, deploy_id, dto);
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDeployInstance), new { project_id = project_id, deploy_id = deploy_id, instance_id = id}, null);
        }
        [HttpPut("{project_id}/deploys/{deploy_id}/instances/{instance_id}")]
        public async Task<IActionResult> UpdateDeployInstance(
            [FromRoute] int project_id, [FromRoute] int deploy_id, [FromRoute] Guid instance_id, [FromBody] UpdateInstanceDto dto)
        {
            var command = new UpdateDeployInstance(project_id,deploy_id,instance_id,dto);
            await _mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{project_id}/deploys/{deploy_id}/instances/{instance_id}")]
        public async Task<IActionResult> RemoveDeployInstance([FromRoute] RemoveDeployInstance command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
