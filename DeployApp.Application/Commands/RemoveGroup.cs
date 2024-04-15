using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveGroup(int group_id) : IRequest;
    
}
