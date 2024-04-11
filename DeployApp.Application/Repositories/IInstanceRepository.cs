using DeployApp.Application.Dtos;
using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface IInstanceRepository
    {
        IQueryable<Instance> GetAllAsIQueryable(int projectId);
        Task<int> CreateInstanceAsync(Instance instance);
        Task UpdateInstanceAsync(Instance instance);
    }
}
