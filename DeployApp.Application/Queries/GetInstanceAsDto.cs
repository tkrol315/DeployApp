using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetInstanceAsDto(int project_id, Guid instance_id) : IRequest<GetInstanceDto>;
}
