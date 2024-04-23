using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record CreateDeploy(int project_id, CreateDeployDto dto) : IRequest<int>;
}
