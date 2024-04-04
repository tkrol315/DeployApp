using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateTag(int id, UpdateTagDto updateTagDto) : IRequest;
  


}
