using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using MediatR;

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

            var versionParts = request.dto.VersionString.Split('.');
            if(versionParts.Length != 3)
                throw new ProjectVersionFormatException(request.dto.VersionString);
            if (int.TryParse(versionParts[0], out var major) &&
                   int.TryParse(versionParts[1], out var minor) &&
                   int.TryParse(versionParts[2], out var patch))
            {
                projectVersion.Major = major;
                projectVersion.Minor = minor;
                projectVersion.Patch = patch;
                projectVersion.Description = request.dto.Description;
                await _projectRepository.UpdateProjectAsync(project);
            }
            else
                throw new ProjectVersionParseException(request.dto.VersionString);
          
        }
    }
}
