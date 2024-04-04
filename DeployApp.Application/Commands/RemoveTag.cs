using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveTag(int id) : IRequest;
    
}
