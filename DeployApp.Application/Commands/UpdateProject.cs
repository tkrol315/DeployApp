using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateProject(int project_id, UpdateProjectDto dto) : IRequest;
  
}
