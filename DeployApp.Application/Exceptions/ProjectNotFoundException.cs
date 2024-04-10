using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class ProjectNotFoundException : DeployAppException
    {
        public ProjectNotFoundException(int id) : base($"Project with id: {id} not found", 404)
        {
        }
    }
}
