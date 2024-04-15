using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetTagAsDto(int tag_id) : IRequest<GetTagDto>;
   
}
