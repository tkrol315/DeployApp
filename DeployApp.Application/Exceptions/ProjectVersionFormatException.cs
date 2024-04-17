using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class ProjectVersionFormatException : DeployAppException
    {
        public ProjectVersionFormatException(string passedVersion) : base($"Value: {passedVersion} is not a valid version format, correct format <Major.Minor.Patch>", 400)
        {
        }
    }
}
