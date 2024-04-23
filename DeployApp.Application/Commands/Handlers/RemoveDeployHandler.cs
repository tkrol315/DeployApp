using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class RemoveDeployHandler : IRequestHandler<RemoveDeploy>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveDeployHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(RemoveDeploy request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithDeploysByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var deploy = project.Deploys.FirstOrDefault(d => d.Id == request.deploy_id)
                ?? throw new DeployNotFoundException(request.deploy_id);
            
            project.Deploys.Remove(deploy);
            await _projectRepository.UpdateProjectAsync(project);   
        }
    }
}
