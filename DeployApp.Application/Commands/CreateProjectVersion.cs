using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record CreateProjectVersion(int project_id, CreateProjectVersionDto dto) : IRequest<int>;
}
