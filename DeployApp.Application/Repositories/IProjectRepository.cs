using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface IProjectRepository
    {
        Task<bool> ProjectWithTitleAlreadyExistsAsync(string title);
        Task<bool> ProjectWithIdAlredyExistsAsync(int id);
        Task<int> CreateProjectAsync(Project project);
        IQueryable<Project> GetProjectsAsIQueryable();
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> GetProjectWithInstancesAndProjectVersionsByIdAsync(int id);
        Task<Project> GetProjectWithInstancesByIdAsync(int id);
        Task RemoveProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
    }
}
