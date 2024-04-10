using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class RemoveProjectHandler : IRequestHandler<RemoveProject>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(RemoveProject request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.id)
                ?? throw new ProjectNotFoundException(request.id);
            await _projectRepository.RemoveProjectAsync(project);
        }
    }
}
