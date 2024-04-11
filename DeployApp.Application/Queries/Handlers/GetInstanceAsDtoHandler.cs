using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetInstanceAsDtoHandler : IRequestHandler<GetInstanceAsDto, GetInstanceDto>
    {
        private readonly IProjectRepository _projectRepository;

        public GetInstanceAsDtoHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetInstanceDto> Handle(GetInstanceAsDto request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndProjectVersionsByIdAsync(request.ProjectId)
                ?? throw new ProjectNotFoundException(request.ProjectId);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.InstanceId)
                ?? throw new InstanceNotFoundException(request.InstanceId);
            return new GetInstanceDto(
                instance.Id,
                instance.Project.Id,
                instance.Type.Description,
                instance.Key,
                instance.Secret,
                instance.InstanceTags.Select(it => new GetTagDto(it.Tag.Id, it.Tag.Name, it.Tag.Description)),
                instance.InstanceGroups.Select(ig => new GetGroupDto(ig.Group.Id, ig.Group.Name, ig.Group.Description)),
                instance.ProjectVersion == null ? null : new GetProjectVersionDto(
                    instance.ProjectVersion.Major,
                    instance.ProjectVersion.Minor,
                    instance.ProjectVersion.Patch,
                    instance.ProjectVersion.Description)
                );
        }
    }
}
