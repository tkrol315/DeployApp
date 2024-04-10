using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveProject(int id) : IRequest;
}
