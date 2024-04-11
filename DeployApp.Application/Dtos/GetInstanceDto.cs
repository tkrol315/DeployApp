using DeployApp.Domain.Entities;

namespace DeployApp.Application.Dtos
{
    public record GetInstanceDto(
        int Id,
        int ProjectId,
        string TypeDescription,
        string Key,
        string Secret,
        IEnumerable<GetTagDto> Tags,
        IEnumerable<GetGroupDto> Groups,
        GetProjectVersionDto? ProjectVersion
        );
}


