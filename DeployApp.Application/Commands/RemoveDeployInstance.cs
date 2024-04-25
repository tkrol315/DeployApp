using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveDeployInstance(int project_id, int deploy_id, int instance_id) : IRequest;
}
