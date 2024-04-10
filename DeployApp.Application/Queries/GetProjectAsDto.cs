using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetProjectAsDto(int id) : IRequest<GetProjectDto>;
}
