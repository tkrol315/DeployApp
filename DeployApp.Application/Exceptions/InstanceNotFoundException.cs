using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class InstanceNotFoundException : DeployAppException
    {
        public InstanceNotFoundException(Guid instanceId) : base($"Instance with id: {instanceId} not found", 404)
        {
        }
        
    }
}
