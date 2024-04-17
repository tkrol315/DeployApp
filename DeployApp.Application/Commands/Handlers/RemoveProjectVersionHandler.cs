using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class RemoveProjectVersionHandler : IRequestHandler<RemoveProjectVersion>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveProjectVersionHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(RemoveProjectVersion request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var versionToRemove = project.ProjectVersions.FirstOrDefault(v => v.Id == request.version_id)
                ?? throw new ProjectVersionNotFoundException(request.version_id);
            project.ProjectVersions.Remove(versionToRemove);

            await _projectRepository.UpdateProjectAsync(project);
        }
    }
}
