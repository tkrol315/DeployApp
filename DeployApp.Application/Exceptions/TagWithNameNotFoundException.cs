using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class TagWithNameNotFoundException : DeployAppException
    {
        public TagWithNameNotFoundException(string tagName)
            : base($"Tag with name: {tagName}, not found", 404)
        {
        }
    }
}
