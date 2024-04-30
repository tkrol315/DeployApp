using DeployApp.Application.Dtos;
using DeployApp.Domain.Enums;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetDeployInstancesAsDtos(int project_id, int deploy_id, Status? status) : IRequest<List<GetInstanceDto>>;
}
