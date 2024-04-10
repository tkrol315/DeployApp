using DeployApp.Application.Dtos;
using DeployApp.Domain.Entities;

namespace DeployApp.Application.Repositories
{
    public interface IInstanceRepository
    {
        //Task<IEnumerable<Instance>> GetAll(int projectId);
        IQueryable<Instance> GetAll(int projectId);
        Task<int> CreateInstanceAsync(Instance instance);
    }
}
