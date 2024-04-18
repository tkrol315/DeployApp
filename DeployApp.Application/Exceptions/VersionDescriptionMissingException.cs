using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class VersionDescriptionMissingException : DeployAppException
    {
        public VersionDescriptionMissingException(string versionString)
            : base($"Version with VersionString: {versionString} not found, provide also VersionDescription to create new ProjectVersion", 400)
        {
        }
    }
}
