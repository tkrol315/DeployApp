namespace DeployApp.Application.Dtos
{
    public record UpdateDeployDto
        (
            string VersionString,
            DateTime Start,
            DateTime End,
            bool IsActive
        );
}
