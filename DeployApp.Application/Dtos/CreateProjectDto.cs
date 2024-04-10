namespace DeployApp.Application.Dtos
{
    public record CreateProjectDto(
        string Title, 
        string Description,
        bool IsActive,
        string YtCode,
        string RepositoryUrl
        );
    
}
