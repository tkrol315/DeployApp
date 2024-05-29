using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetProjectsAsDtos(ProjectFilterDto searchPhrase) : IRequest<List<GetProjectDto>>;
}
