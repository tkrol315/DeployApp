namespace DeployApp.Application.Dtos
{
    public record UpdateInstanceDto
        (
            string TypeDescription,
            string Name,
            string Key,
            string Secret,
            string? VersionString,
            string? VersionDescription
        );

    
}
