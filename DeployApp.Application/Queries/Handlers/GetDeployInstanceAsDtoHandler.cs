using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetDeployInstanceAsDtoHandler : IRequestHandler<GetDeployInstanceAsDto, GetInstanceDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _projectVersionConverter;

        public GetDeployInstanceAsDtoHandler(IProjectRepository projectRepository, IProjectVersionConverter projectVersionConverter)
        {
            _projectRepository = projectRepository;
            _projectVersionConverter = projectVersionConverter;
        }

        public async Task<GetInstanceDto> Handle(GetDeployInstanceAsDto request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithDeployAndInstancesAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var deploy = project.Deploys.FirstOrDefault(d => d.Id == request.deploy_id)
                ?? throw new DeployNotFoundException(request.deploy_id);
            var deployInstance = deploy.DeployInstances.FirstOrDefault(di => di.InstanceId ==  request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            var i = deployInstance.Instance;
            var dto = new GetInstanceDto(
                  i.Id,
                  i.ProjectId,
                  i.Type.Description,
                  i.Name,
                  i.Key,
                  i.Secret,
                  i.InstanceTags.Select(it =>
                                new GetTagDto(it.Tag.Id, it.Tag.Name, it.Tag.Description)
                      ),
                  i.InstanceGroups.Select(ig =>
                                new GetGroupDto(ig.Group.Id, ig.Group.Name, ig.Group.Description)
                      ),
                  i.ProjectVersion == null ? null :
                        new GetProjectVersionDto(
                            i.ProjectVersion.Id,
                            _projectVersionConverter.VersionToVersionString(i.ProjectVersion.Major, i.ProjectVersion.Minor, i.ProjectVersion.Patch),
                            i.ProjectVersion.Description
                            )
                ) ;
            return dto;
        }
    }
}
