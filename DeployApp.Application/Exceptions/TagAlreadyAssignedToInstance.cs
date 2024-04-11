using DeployApp.Application.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DeployApp.Application.Exceptions
{
    public class TagAlreadyAssignedToInstance : DeployAppException
    {
        public TagAlreadyAssignedToInstance(int instanceId, int tagId) 
            : base($"Instance with id: {instanceId} already contains tag with id: {tagId}", 400)
        {
        }
    }
}
