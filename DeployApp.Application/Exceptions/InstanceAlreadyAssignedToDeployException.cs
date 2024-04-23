using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class InstanceAlreadyAssignedToDeployException : DeployAppException
    {
        public InstanceAlreadyAssignedToDeployException(int deployId, string instanceName)
            : base($"Instance with name: {instanceName} already assigned to deploy with id: {deployId}", 400)
        {
        }
    }
}
