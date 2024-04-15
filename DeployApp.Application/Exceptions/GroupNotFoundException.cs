using DeployApp.Application.Abstractions;
using DeployApp.Domain.Entities;

namespace DeployApp.Application.Exceptions
{
    public class GroupNotFoundException : DeployAppException
    {
        public GroupNotFoundException(int groupId) 
            : base($"Group with id: {groupId}, not found", 404)
        {
        }
    }
}
