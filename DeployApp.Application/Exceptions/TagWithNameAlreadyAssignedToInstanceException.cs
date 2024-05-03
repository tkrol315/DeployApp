using DeployApp.Application.Abstractions;
using DeployApp.Domain.Entities;

namespace DeployApp.Application.Exceptions
{
    public class TagWithNameAlreadyAssignedToInstanceException : DeployAppException
    {
        public TagWithNameAlreadyAssignedToInstanceException(Guid instanceId, string tagName)
            : base($"Instance with id: {instanceId} already contains tag with name: {tagName}", 400)
        {
        }
    }
}
