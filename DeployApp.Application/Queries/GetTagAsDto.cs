using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetTagAsDto(int id) : IRequest<GetTagDto>;
   
}
