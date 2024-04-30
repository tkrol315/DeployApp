using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class ProjectWithTitleAlreadyExistsException : DeployAppException
    {
        public ProjectWithTitleAlreadyExistsException(string title)
            : base($"Project with title: {title} already exists", 400)
        {
        }
    }
}
