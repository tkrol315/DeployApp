using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetDeployInstancesAsDtosHandler : IRequestHandler<GetDeployInstancesAsDtos, List<GetInstanceDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public GetDeployInstancesAsDtosHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task<List<GetInstanceDto>> Handle(GetDeployInstancesAsDtos request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithDeployAndInstancesAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var deploy = project.Deploys.FirstOrDefault(d => d.Id == request.deploy_id)
                ?? throw new DeployNotFoundException(request.deploy_id);
            var instances = deploy.DeployInstances
                .Where(di => !request.status.HasValue || di.Status == request.status.Value)
                .Select(di =>
                new GetInstanceDto(
                    di.Instance.Id,
                    di.Instance.ProjectId,
                    di.Instance.Type.Description,
                    di.Instance.Name,
                    di.Instance.Key,
                    di.Instance.Secret,
                    di.Instance.InstanceTags.Select(it => 
                        new GetTagDto(it.Tag.Id, it.Tag.Name, it.Tag.Description)
                    ).ToList(),
                    di.Instance.InstanceGroups.Select(ig =>
                        new GetGroupDto(ig.Group.Id, ig.Group.Name, ig.Group.Description)
                    ).ToList(),
                    di.Instance.ProjectVersion == null ? null : 
                        new GetProjectVersionDto
                            (
                            di.Instance.ProjectVersion.Id,
                            _converter.VersionToVersionString(di.Instance.ProjectVersion.Major, di.Instance.ProjectVersion.Minor, di.Instance.ProjectVersion.Patch),
                            di.Instance.ProjectVersion.Description
                            )
                    )
            ).ToList();
            return instances;

        }
    }
}
