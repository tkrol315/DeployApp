using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record AssignInstanceToDeploy(int project_id, int deploy_id, AssignInstanceToDeployDto dto) : IRequest<int>;
}
