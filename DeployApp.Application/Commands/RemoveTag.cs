using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveTag(int tag_id) : IRequest;
    
}
