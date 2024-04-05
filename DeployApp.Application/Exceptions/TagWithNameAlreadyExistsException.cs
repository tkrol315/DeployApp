using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class TagWithNameAlreadyExistsException : DeployAppException
    {
        public TagWithNameAlreadyExistsException(string tagName) : base($"Tag with name: {tagName} already exists", 400)
        {
        }
    }
}
