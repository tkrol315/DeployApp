namespace DeployApp.Application.Dtos
{
    public record CreateDeployDto(
        string VersionString,
        DateTime Start,
        DateTime End,
        string Description,
        bool IsActive
        );
}
