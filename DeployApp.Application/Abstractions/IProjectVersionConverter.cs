using DeployApp.Domain.Enums;

namespace DeployApp.Application.Abstractions
{
    public interface IProjectVersionConverter
    {
        Dictionary<VersionParts, int> VersionStringToDictionary(string versionString);
        string VersionToVersionString(int major, int minor, int patch);
    }
}
