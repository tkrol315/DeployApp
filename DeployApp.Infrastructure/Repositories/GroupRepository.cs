using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DeployAppDbContext _context;

        public GroupRepository(DeployAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateGroupAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group.Id;
        }

        public async Task<Group> GetGroupByIdAsync(int id)
            => await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);

        public async Task<Group> GetGroupByNameAsync(string name)
            => await _context.Groups.FirstOrDefaultAsync(g => g.Name == name);

        public IQueryable<Group> GetGroupsAsIQueryable()
            => _context.Groups;

        public async Task<bool> GroupWithNameAlreadyExistsAsync(string name)
            => await _context.Groups.AnyAsync(g => g.Name == name);

        public async Task RemoveGroupAsync(Group group)
        {
            _context.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroupAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }
    }
}
