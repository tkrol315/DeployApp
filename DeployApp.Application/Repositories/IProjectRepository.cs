using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface IProjectRepository
    {
        Task<bool> ProjectWithTitleAlreadyExistsAsync(string title);
        Task<int> CreateProjectAsync(Project project);
        IQueryable<Project> GetProjectsAsIQueryable();
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> GetProjectWithInstancesAndProjectVersionsByIdAsync(int id);
        Task<Project> GetProjectWithProjectVersionsByIdAsync(int id);
        Task<Project> GetProjectWithInstancesByIdAsync(int id);
        Task<Project> GetProjectWithInstancesAndTagsByIdAsync(int id);
        Task<Project> GetProjectWithInstancesAndGroupsByIdAsync(int id);
        Task RemoveProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task<Project> GetProjectWithDeploysByIdAsync(int id);
        Task<Project> GetProjectWithDeploysAndProjectVersionsByIdAsync(int id);
        Task<Project> GetProjectWithDeployAndInstancesAsync(int id);
        Task<Project> GetProjectWithInstancesAndDeploysWithInstancesByIdAsnyc(int id);
    }
}
