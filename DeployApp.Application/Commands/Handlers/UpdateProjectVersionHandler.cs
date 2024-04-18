using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Application.Utils;
using DeployApp.Domain.Enums;
using MediatR;
using System.Security;

namespace DeployApp.Application.Commands.Handlers
{
    public class UpdateProjectVersionHandler : IRequestHandler<UpdateProjectVersion>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectVersionHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(UpdateProjectVersion request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var projectVersion = project.ProjectVersions.FirstOrDefault(v => v.Id == request.version_id)
                ?? throw new ProjectVersionNotFoundException(request.version_id);

            var versionDic = ProjectVersionConverter.VersionStringToDictionary(request.dto.VersionString);
            projectVersion.Major = versionDic[VersionParts.Major];
            projectVersion.Minor = versionDic[VersionParts.Minor];
            projectVersion.Patch = versionDic[VersionParts.Patch];
            projectVersion.Description = request.dto.Description;
            await _projectRepository.UpdateProjectAsync(project);
          
        }
    }
}
