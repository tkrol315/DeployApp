using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{ 
    public record CreateTag(CreateTagDto createTagDto) : IRequest;
}
