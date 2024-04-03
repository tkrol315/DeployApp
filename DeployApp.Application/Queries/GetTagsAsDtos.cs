using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetTagsAsDtos() : IRequest<List<GetTagDto>>;
    
}
