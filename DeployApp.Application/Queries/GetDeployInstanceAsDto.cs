using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetDeployInstanceAsDto(int project_id, int deploy_id, int instance_id) : IRequest<GetInstanceDto>;
}
