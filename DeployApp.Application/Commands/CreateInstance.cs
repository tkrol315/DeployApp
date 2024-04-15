using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record CreateInstance(int project_id, CreateInstanceDto dto) : IRequest<int>;
}
