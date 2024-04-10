using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateProject(int id, UpdateProjectDto dto) : IRequest;
  
}
