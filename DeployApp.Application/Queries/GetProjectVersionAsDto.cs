using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetProjectVersionAsDto(int project_id, int version_id) : IRequest<GetProjectVersionDto>;
    
}
