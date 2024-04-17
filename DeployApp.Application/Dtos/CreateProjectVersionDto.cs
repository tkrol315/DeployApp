namespace DeployApp.Application.Dtos
{
    public record CreateProjectVersionDto(
            string VersionString,
            string Description
        );
}
