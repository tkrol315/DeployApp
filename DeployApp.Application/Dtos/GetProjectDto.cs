using DeployApp.Domain.Entities;

namespace DeployApp.Application.Dtos
{
    public record GetProjectDto(
        int id,
        string Title,
        string Description,
        bool IsActive,
        string YtCode,
        string RepositoryUrl
        );
}


