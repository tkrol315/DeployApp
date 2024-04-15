using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetProjectAsDto(int project_id) : IRequest<GetProjectDto>;
}
