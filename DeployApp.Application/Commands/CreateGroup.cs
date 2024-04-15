using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record CreateGroup(CreateGroupDto dto) : IRequest<int>;
}
