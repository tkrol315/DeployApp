using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetDeployInstancesAsDtos(int project_id, int deploy_id, string? status) : IRequest<List<GetInstanceDto>>;
}
