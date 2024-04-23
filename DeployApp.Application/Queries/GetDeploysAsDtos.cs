using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record  GetDeploysAsDtos(int project_id):IRequest<List<GetDeployDto>>;
}
