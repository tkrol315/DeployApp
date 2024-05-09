namespace DeployApp.Application.Dtos
{
    public record UpdateDeployDto
        (
            string VersionString,
            DateTime Start,
            DateTime End,
            string Description,
            bool IsActive
        );
}
