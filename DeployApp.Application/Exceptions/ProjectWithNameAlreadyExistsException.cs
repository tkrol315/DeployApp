using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class ProjectWithNameAlreadyExistsException : DeployAppException
    {
        public ProjectWithNameAlreadyExistsException(string title)
            : base($"Project with title: {title} already exists", 400)
        {
        }
    }
}
