using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetInstanceAsDto(int ProjectId, int InstanceId) : IRequest<GetInstanceDto>;
}
