using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveInstance(int project_id, Guid instance_id) : IRequest;
}
