using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetDeployAsDtoHandler : IRequestHandler<GetDeployAsDto, GetDeployDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public GetDeployAsDtoHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task<GetDeployDto> Handle(GetDeployAsDto request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithDeploysByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var deploy = project.Deploys.FirstOrDefault(d => d.Id == request.deploy_id)
                ?? throw new DeployNotFoundException(request.deploy_id);
            var dto = new GetDeployDto(
                deploy.Id,
                deploy.ProjectId,
                deploy.ProjectVersionId,
                _converter.VersionToVersionString(deploy.ProjectVersion.Major, deploy.ProjectVersion.Minor, deploy.ProjectVersion.Patch),
                deploy.Start,
                deploy.End,
                deploy.Description,
                deploy.IsActive
                );
            return dto;
        }
    }
}
