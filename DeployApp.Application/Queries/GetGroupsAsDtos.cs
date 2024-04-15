using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetGroupsAsDtos : IRequest<List<GetGroupDto>>;
   
}
