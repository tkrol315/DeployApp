using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateInstance(int project_id, int instance_id, UpdateInstanceDto dto) : IRequest;
    
}
