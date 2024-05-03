using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record AssignTag(int project_id, Guid instance_id, AssignTagDto dto) : IRequest;
}
