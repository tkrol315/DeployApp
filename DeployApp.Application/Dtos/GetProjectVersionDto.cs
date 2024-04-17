namespace DeployApp.Application.Dtos
{
    public record GetProjectVersionDto(
        int Id,
        string VersionString,
        string Description
        );
}
