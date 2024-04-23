namespace DeployApp.Application.Dtos
{
    public record CreateDeployDto(
        string VersionString,
        DateTime Start,
        DateTime End,
        bool IsActive
        );
}
