using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface IGroupRepository
    {
        IQueryable<Group> GetGroupsAsIQueryable();
        Task<Group> GetGroupByIdAsync(int id);
        Task<Group> GetGroupByNameAsync(string name);
        Task<int> CreateGroupAsync(Group group);
        Task<bool> GroupWithNameAlreadyExistsAsync(string name);
        Task UpdateGroupAsync(Group group);
        Task RemoveGroupAsync(Group group);
    }
}
