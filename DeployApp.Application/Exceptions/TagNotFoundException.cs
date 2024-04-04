using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class TagNotFoundException : DeployAppException
    {
        public TagNotFoundException(int tagId) : base($"Tag with id: {tagId}, not found", 404)
        {
        }
    }
}
