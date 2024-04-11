using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class InstanceNotFoundException : DeployAppException
    {
        public InstanceNotFoundException(int id) : base($"Instance with id: {id} not found", 404)
        {
        }
    }
}
