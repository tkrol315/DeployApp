using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class ProjectVersionParseException : DeployAppException
    {
        public ProjectVersionParseException(string passedValue) : base($"Value: {passedValue} cannot be parsed to the ProjectVersion", 400)
        {
        }
    }
}
