using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DeployAppDbContext _context;
        public TagRepository(DeployAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag.Id;
        }

        public Task<Tag> GetTagByIdAsync(int id)
            => _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<IEnumerable<Tag>> GetTagsAsync()
            => await _context.Tags.ToListAsync();

        public async Task RemoveTagAsync(Tag tag)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TagWithNameAlreadyExistsAsync(string name)
            => await _context.Tags.AnyAsync(t => t.Name == name);

        public async Task UpdateTagAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }
    }
}
