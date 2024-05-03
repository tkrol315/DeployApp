using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class AssignInstanceToDeployHandler : IRequestHandler<AssignInstanceToDeploy, Guid>
    {
        private readonly IProjectRepository _projectRepository;

        public AssignInstanceToDeployHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> Handle(AssignInstanceToDeploy request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var deploy = project.Deploys.FirstOrDefault(d => d.Id == request.deploy_id)
                ?? throw new DeployNotFoundException(request.deploy_id);
            if(deploy.DeployInstances.Any(i => i.Instance.Id == request.dto.InstanceId)) 
                throw new InstanceAlreadyAssignedToDeployException(deploy.Id,request.dto.InstanceId);
            var instanceToAssign = project.Instances.FirstOrDefault(i => i.Id == request.dto.InstanceId)
                ?? throw new InstanceNotFoundException(request.dto.InstanceId);
            var deployInstance = new DeployInstance();
            deployInstance.Instance = instanceToAssign;
            deployInstance.Deploy = deploy;
            deployInstance.Status = request.dto.Status;
            deploy.DeployInstances.Add(deployInstance);
            await _projectRepository.UpdateProjectAsync(project);
            return deployInstance.InstanceId;
        }
    }
}
