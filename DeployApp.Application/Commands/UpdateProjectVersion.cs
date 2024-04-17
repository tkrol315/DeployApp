using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateProjectVersion(int project_id, int version_id, UpdateProjectVersionDto dto) : IRequest;
    
}
