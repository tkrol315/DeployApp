using DeployApp.Application.Abstractions;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Domain.Enums;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateProjectVersionHandler : IRequestHandler<CreateProjectVersion, int>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public CreateProjectVersionHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task<int> Handle(CreateProjectVersion request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);

            var versionDic = _converter.VersionStringToDictionary(request.dto.VersionString);

            var projectVersion = new ProjectVersion()
            {
                ProjectId = request.project_id,
                Major = versionDic[VersionParts.Major],
                Minor = versionDic[VersionParts.Minor],
                Patch = versionDic[VersionParts.Patch],
                Description = request.dto.Description,
                Instances = new List<Instance>()
            };
            project.ProjectVersions.Add(projectVersion);
            await _projectRepository.UpdateProjectAsync(project);
            return projectVersion.Id;

        }
    }
}
