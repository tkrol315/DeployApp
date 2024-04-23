using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetDeployAsDto(int project_id, int deploy_id) : IRequest<GetDeployDto>;
}
