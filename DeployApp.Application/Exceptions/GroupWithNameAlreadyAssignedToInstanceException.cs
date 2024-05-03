using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class GroupWithNameAlreadyAssignedToInstanceException : DeployAppException
    {
        public GroupWithNameAlreadyAssignedToInstanceException(Guid instanceId, string groupName)
            : base($"Instance with id: {instanceId} already contains group with name: {groupName}", 400)
        {
        }
    }
}
