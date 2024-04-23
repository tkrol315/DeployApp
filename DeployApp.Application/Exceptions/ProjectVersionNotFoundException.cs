using DeployApp.Application.Abstractions;
using System.Security;

namespace DeployApp.Application.Exceptions
{
    public class ProjectVersionNotFoundException : DeployAppException
    {
        public ProjectVersionNotFoundException(int project_id) 
            : base($"Project version with id: {project_id}, not found", 404)
        {
        }
        public ProjectVersionNotFoundException(string versionString)
            : base($"Project version: {versionString}, not found", 404)
        {

        }
    }
}
