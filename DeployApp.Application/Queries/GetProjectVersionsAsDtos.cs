using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetProjectVersionsAsDtos(int project_id) : IRequest<List<GetProjectVersionDto>>;
}
