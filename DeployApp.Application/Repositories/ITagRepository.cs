using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface ITagRepository
    {
        Task<int> AddTagAsync(Tag tag);
        Task<Tag> GetTagByIdAsync(int id);
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task<bool> TagWithNameAlreadyExistsAsync(string name);
        Task RemoveTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
    }
}
