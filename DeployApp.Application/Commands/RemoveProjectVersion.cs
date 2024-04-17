using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveProjectVersion(int project_id, int version_id) : IRequest;
}
