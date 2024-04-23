using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DeployAppDbContext _context;

        public ProjectRepository(DeployAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project.Id;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
            => await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

        public IQueryable<Project> GetProjectsAsIQueryable()
            => _context.Projects;
        public async Task<Project> GetProjectWithInstancesAndProjectVersionsByIdAsync(int id)
            => await _context.Projects
                .Include(p => p.Instances)
                    .ThenInclude(i => i.Type)
                .Include(p => p.Instances)
                    .ThenInclude(i => i.InstanceTags)
                        .ThenInclude(it => it.Tag)
                .Include(p => p.Instances)
                    .ThenInclude(i => i.InstanceGroups)
                        .ThenInclude(ig => ig.Group)
                .Include(p => p.ProjectVersions)
                .FirstOrDefaultAsync(p => p.Id == id);


        public async Task<bool> ProjectWithTitleAlreadyExistsAsync(string title)
            => await _context.Projects.AnyAsync(p => p.Title == title);

        public async Task RemoveProjectAsync(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Project> GetProjectWithInstancesAndTagsByIdAsync(int id)
            => await _context.Projects
                .Include(p => p.Instances)
                    .ThenInclude(i => i.InstanceTags)
                        .ThenInclude(it => it.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Project> GetProjectWithInstancesAndGroupsByIdAsync(int id)
            => await _context.Projects
                .Include(p => p.Instances)
                    .ThenInclude(i => i.InstanceGroups)
                        .ThenInclude(ig => ig.Group)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Project> GetProjectWithInstancesByIdAsync(int id)
            => await _context.Projects
            .Include(p => p.Instances)
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Project> GetProjectWithProjectVersionsByIdAsync(int id)
            => await _context.Projects
            .Include(p => p.ProjectVersions.AsQueryable())
            .FirstOrDefaultAsync(p => p.Id == id);

        public Task<Project> GetProjectWithDeploysByIdAsync(int id)
            => _context.Projects.
            Include(p => p.ProjectVersions)
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.InstanceTags)
                            .ThenInclude(it => it.Tag)
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.InstanceGroups)
                            .ThenInclude(ig => ig.Group)
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.ProjectVersion)
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.Type)
            .FirstOrDefaultAsync(p => p.Id == id);

        public  Task<Project> GetProjectWithDeploysAndProjectVersionsByIdAsync(int id)
            =>  _context.Projects
            .Include(p => p.ProjectVersions)
            .Include(p => p.Deploys)
            .FirstOrDefaultAsync(p => p.Id == id);

        public Task<Project> GetProjectWithDeployAndInstancesAsync(int id)
            => _context.Projects
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.InstanceTags)
                            .ThenInclude(it => it.Tag)
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.InstanceGroups)
                            .ThenInclude(it => it.Group)
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.ProjectVersion)
            .Include(p => p.Deploys)
                .ThenInclude(d => d.DeployInstances)
                    .ThenInclude(di => di.Instance)
                        .ThenInclude(i => i.Type)
            .FirstOrDefaultAsync(p => p.Id == id);

        public Task<Project> GetProjectWithInstancesAndDeploysWithInstancesByIdAsnyc(int id)
            => _context.Projects
                .Include(p => p.Deploys)
                    .ThenInclude(d => d.DeployInstances)
                        .ThenInclude(di => di.Instance)
                .Include(p => p.Instances)
                .FirstOrDefaultAsync(p => p.Id == id);


    }
}
