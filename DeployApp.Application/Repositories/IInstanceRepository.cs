using DeployApp.Application.Dtos;
using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface IInstanceRepository
    {
        IQueryable<Instance> GetAllAsIQueryable(int projectId);
        Task<Guid> CreateInstanceAsync(Instance instance);
        Task UpdateInstanceAsync(Instance instance);
        Task<bool> InstanceWithNameAlreadyExists(int projectId, string name);
    }
}
