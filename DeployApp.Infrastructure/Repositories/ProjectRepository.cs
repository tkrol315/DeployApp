using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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

        public async Task<IEnumerable<Project>> GetProjectsAsync()
            => await _context.Projects.ToListAsync();

        public async Task<bool> ProjectWithIdAlredyExistsAsync(int id)
            => await _context.Projects.AnyAsync(p => p.Id == id);

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
    }
}
