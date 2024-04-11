namespace DeployApp.Application.Dtos
{
    public record GetProjectVersionDto(
        int Major,
        int Minor,
        int Patch,
        string Description
        );
}
