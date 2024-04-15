using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface ITagRepository
    {
        Task<int> AddTagAsync(Tag tag);
        Task<Tag> GetTagByIdAsync(int id);
        Task<Tag> GetTagByNameAsync(string name);
        IQueryable<Tag> GetTagsAsIQueryable();
        Task<bool> TagWithNameAlreadyExistsAsync(string name);
        Task<bool> TagExistsByIdAsync(int id);
        Task RemoveTagAsync(Tag tag);
        Task UpdateTagAsync(Tag tag);
    }
}
