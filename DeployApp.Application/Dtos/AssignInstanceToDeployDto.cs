using DeployApp.Domain.Enums;

namespace DeployApp.Application.Dtos
{
    public record AssignInstanceToDeployDto(
        int InstanceId,
        Status Status
        );
}
