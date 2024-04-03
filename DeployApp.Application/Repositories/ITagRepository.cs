using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface ITagRepository
    {
        Task AddTagAsync(Tag tag);
        Task<IEnumerable<Tag>> GetTagsAsync();
    }
}
