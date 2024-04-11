using DeployApp.Application.Dtos;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Infrastructure.Repositories
{
    public class InstanceRepository : IInstanceRepository
    {
        private readonly DeployAppDbContext _context;

        public InstanceRepository(DeployAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateInstanceAsync(Instance instance)
        {
            await _context.Instances.AddAsync(instance);
            await _context.SaveChangesAsync();
            return instance.Id;
        }

        public IQueryable<Instance> GetAllAsIQueryable(int projectId)
        =>  _context.Instances
            .Include(i => i.Project)
            .Include(i => i.Type)
            .Include(i => i.ProjectVersion)
            .Include(i => i.InstanceGroups)
                .ThenInclude(ig => ig.Group)
            .Include(i => i.InstanceTags)
                .ThenInclude(it => it.Tag)
            .Where(i => i.ProjectId == projectId);

        public async Task UpdateInstanceAsync(Instance instance)
        {
            _context.Instances.Update(instance);
            await _context.SaveChangesAsync();
        }
    }
}
