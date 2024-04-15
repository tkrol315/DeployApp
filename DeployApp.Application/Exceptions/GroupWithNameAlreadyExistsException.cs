using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class GroupWithNameAlreadyExistsException : DeployAppException
    {
        public GroupWithNameAlreadyExistsException(string groupName) 
            : base($"Group with name: {groupName} already exists", 400)
        {
        }
    }
}
