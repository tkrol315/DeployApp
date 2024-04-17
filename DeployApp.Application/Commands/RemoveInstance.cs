using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveInstance(int project_id, int instance_id) : IRequest;
}
