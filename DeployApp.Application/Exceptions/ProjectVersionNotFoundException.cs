using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class ProjectVersionNotFoundException : DeployAppException
    {
        public ProjectVersionNotFoundException(int project_id) 
            : base($"Project version with id: {project_id}, not found", 404)
        {
        }
    }
}
