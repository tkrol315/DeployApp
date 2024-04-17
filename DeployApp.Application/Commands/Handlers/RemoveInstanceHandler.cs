using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class RemoveInstanceHandler : IRequestHandler<RemoveInstance>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveInstanceHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(RemoveInstance request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            project.Instances.Remove(instance);
            await _projectRepository.UpdateProjectAsync(project);
        }
    }
}
