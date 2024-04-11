using MediatR;

namespace DeployApp.Application.Commands
{
    public record AssignTagToInstance(int projectId, int instanceId, int tagId) : IRequest;
}
