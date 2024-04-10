namespace DeployApp.Application.Dtos
{
    public record UpdateProjectDto(
        string Title,
        string Description,
        bool IsActive,
        string YtCode,
        string RepositoryUrl);
 
}
