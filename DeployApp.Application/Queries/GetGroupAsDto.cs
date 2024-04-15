using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetGroupAsDto(int group_id) : IRequest<GetGroupDto>;
  
}
