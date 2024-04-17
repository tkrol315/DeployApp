using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class ProjectAlreadyContainsInstanceWithNameException : DeployAppException
    {
        public ProjectAlreadyContainsInstanceWithNameException(int projectId, string name)
            : base($"Project with Id: {projectId} already contains instance with Name: {name}", 400)
        {
        }
    }
}
