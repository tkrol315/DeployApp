namespace DeployApp.Application.Services
{
    public interface ITagReadService
    {
        Task<bool> TagNameAlreadyExistsAsync(string name);
    }
}
