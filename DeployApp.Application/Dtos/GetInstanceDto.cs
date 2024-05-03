using DeployApp.Domain.Entities;

namespace DeployApp.Application.Dtos
{
    public record GetInstanceDto(
        Guid Id,
        int ProjectId,
        string TypeDescription,
        string Name,
        string Key,
        string Secret,
        IEnumerable<GetTagDto> Tags,
        IEnumerable<GetGroupDto> Groups,
        GetProjectVersionDto? ProjectVersion
        );
}


