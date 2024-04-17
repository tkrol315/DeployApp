using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
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
            var versionParts = request.dto.VersionString.Split('.');
            if (versionParts.Length != 3)
                throw new ProjectVersionFormatException(request.dto.VersionString);

            if (int.TryParse(versionParts[0], out var major) &&
                   int.TryParse(versionParts[1], out var minor) &&
                   int.TryParse(versionParts[2], out var patch))
            {
                var projectVersion = new ProjectVersion()
                {
                    ProjectId = request.project_id,
                    Major = major,
                    Minor = minor,
                    Patch = patch,
                    Description = request.dto.Description
                };
                project.ProjectVersions.Add(projectVersion);
                await _projectRepository.UpdateProjectAsync(project);
                return projectVersion.Id;
            }
            else
                throw new ProjectVersionParseException(request.dto.VersionString);
        }
    }
}
