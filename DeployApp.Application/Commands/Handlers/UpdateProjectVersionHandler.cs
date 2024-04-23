using DeployApp.Application.Abstractions;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Enums;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class UpdateProjectVersionHandler : IRequestHandler<UpdateProjectVersion>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;
        public UpdateProjectVersionHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task Handle(UpdateProjectVersion request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var projectVersion = project.ProjectVersions.FirstOrDefault(v => v.Id == request.version_id)
                ?? throw new ProjectVersionNotFoundException(request.version_id);

            var versionDic = _converter.VersionStringToDictionary(request.dto.VersionString);
            projectVersion.Major = versionDic[VersionParts.Major];
            projectVersion.Minor = versionDic[VersionParts.Minor];
            projectVersion.Patch = versionDic[VersionParts.Patch];
            projectVersion.Description = request.dto.Description;
            await _projectRepository.UpdateProjectAsync(project);
          
        }
    }
}
