using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveDeploy(int project_id, int deploy_id) : IRequest;
}
