using DeployApp.Domain.Enums;

namespace DeployApp.Application.Dtos
{
    public record AssignInstanceToDeployDto(
        Guid InstanceId,
        Status Status
        );
}
