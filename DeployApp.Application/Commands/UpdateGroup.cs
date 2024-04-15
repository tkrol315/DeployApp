using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateGroup(int group_id, UpdateGroupDto dto) : IRequest;
}
