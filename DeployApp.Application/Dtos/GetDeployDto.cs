namespace DeployApp.Application.Dtos
{
    public record GetDeployDto(
        int Id,
        int ProjectId,
        int ProjectVersionId,
        string ProjectVersionString,
        DateTime Start,
        DateTime End,
        string Description,
        bool IsActive
        );
}
