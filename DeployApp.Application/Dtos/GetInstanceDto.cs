using DeployApp.Domain.Entities;

namespace DeployApp.Application.Dtos
{
    public record GetInstanceDto(
        int Id,
        int ProjectId,
        string TypeDescription,
        string Key,
        string Secret,
        ICollection<InstanceTag> InstanceTags,
        ICollection<InstanceGroup> InstanceGroups,
        int? Major,
        int? Minor,
        int? Patch
        );
}


