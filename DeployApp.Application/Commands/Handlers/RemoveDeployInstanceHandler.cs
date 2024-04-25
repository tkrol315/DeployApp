using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class RemoveDeployInstanceHandler : IRequestHandler<RemoveDeployInstance>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveDeployInstanceHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(RemoveDeployInstance request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var deploy = project.Deploys.FirstOrDefault(d => d.Id == request.deploy_id)
                ?? throw new DeployNotFoundException(request.deploy_id);
            var deployInstance = deploy.DeployInstances.FirstOrDefault(di => di.InstanceId == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            deploy.DeployInstances.Remove(deployInstance);
            await _projectRepository.UpdateProjectAsync(project);
        }
    }
}
