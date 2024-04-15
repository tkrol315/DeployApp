using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class GroupWithNameNotFoundException : DeployAppException
    {
        public GroupWithNameNotFoundException(string groupName)
            : base($"Group with name: {groupName}, not found", 404)
        {
        }
    }
}
