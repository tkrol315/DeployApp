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
        public async Task AddTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
            => await _context.Tags.ToListAsync();

    }
}
