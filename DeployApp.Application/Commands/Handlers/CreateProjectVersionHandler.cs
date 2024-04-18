using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Application.Utils;
using DeployApp.Domain.Entities;
using DeployApp.Domain.Enums;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateProjectVersionHandler : IRequestHandler<CreateProjectVersion, int>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectVersionHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProjectVersion request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);

            var versionDic = ProjectVersionConverter.VersionStringToDictionary(request.dto.VersionString);

            var projectVersion = new ProjectVersion()
            {
                ProjectId = request.project_id,
                Major = versionDic[VersionParts.Major],
                Minor = versionDic[VersionParts.Minor],
                Patch = versionDic[VersionParts.Patch],
                Description = request.dto.Description
            };
            project.ProjectVersions.Add(projectVersion);
            await _projectRepository.UpdateProjectAsync(project);
            return projectVersion.Id;

        }
    }
}
