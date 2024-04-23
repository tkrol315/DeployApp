using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class DeployNotFoundException : DeployAppException
    {
        public DeployNotFoundException(int deploy_id) 
            : base($"Deploy with id: {deploy_id}, not found", 404)
        {
        }
    }
}
