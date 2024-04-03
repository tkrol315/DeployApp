using DeployApp.Application.Services;
using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Infrastructure.Services
{
    public class TagReadService : ITagReadService
    {
        private readonly DeployAppDbContext _context;

        public TagReadService(DeployAppDbContext context)
        {
            _context = context;
        }

        public Task<bool> TagNameAlreadyExistsAsync(string name)
            => _context.Tags.AnyAsync(t => t.Name == name);
    }
}
