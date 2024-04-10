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

        //To change 
        public IQueryable<Instance> GetAll(int projectId)
        =>  _context.Instances
            .Include(i => i.ProjectVersion)
            .Include(i => i.InstanceGroups)
            .Include(i => i.InstanceTags)
            .Where(i => i.ProjectId == projectId);
        //==

    }
}
