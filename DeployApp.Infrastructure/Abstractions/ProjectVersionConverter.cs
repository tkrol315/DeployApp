using DeployApp.Application.Abstractions;
using DeployApp.Application.Exceptions;
using DeployApp.Domain.Enums;

namespace DeployApp.Infrastructure.Abstractions
{
    public class ProjectVersionConverter : IProjectVersionConverter
    {
        public Dictionary<VersionParts, int> VersionStringToDictionary(string versionString)
        {
            var versionParts = versionString.Split('.');
            if (versionParts.Length != 3)
                throw new ProjectVersionFormatException(versionString);

            if (int.TryParse(versionParts[0], out var major) &&
                   int.TryParse(versionParts[1], out var minor) &&
                   int.TryParse(versionParts[2], out var patch))
            {

                return new Dictionary<VersionParts, int>()
                {
                    { VersionParts.Major, major},
                    { VersionParts.Minor , minor},
                    { VersionParts.Patch , patch}
                };
            }
            else
                throw new ProjectVersionParseException(versionString);
        }

        public string VersionToVersionString(int major, int minor, int patch)
            => string.Join(".", major, minor, patch);
    }
}
