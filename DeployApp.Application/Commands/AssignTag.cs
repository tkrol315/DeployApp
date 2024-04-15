using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record AssignTag(int project_id, int instance_id, AssignTagDto dto) : IRequest;
}
