using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel.DataAnnotations;

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

        //don't work yet 
        [HttpGet]
        public async Task<ActionResult<List<GetInstanceDto>>> GetAll([FromRoute] int project_id, [FromQuery] InstanceSearchPhraseDto searchPhrase) 
        {
            var query = new GetInstancesAsDtos(project_id, searchPhrase);
            await _mediator.Send(query);
            return Ok(query);
        }
        //==========

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
            //change to CreateadAtAction after creating getById 
            return Created();
        }
    }
}
