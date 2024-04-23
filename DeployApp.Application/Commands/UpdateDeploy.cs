using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateDeploy(int project_id, int deploy_id, UpdateDeployDto dto) : IRequest;
    
}
