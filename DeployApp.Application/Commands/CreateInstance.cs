using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record CreateInstance(int projectId, CreateInstanceDto dto) : IRequest<int>;
}
