using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record CreateProject(CreateProjectDto dto) : IRequest<int>;
}
