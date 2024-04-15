using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class AssignGroupHandler : IRequestHandler<AssignGroup>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IInstanceRepository _instanceRepository;
        private readonly IGroupRepository _groupRepository;

        public AssignGroupHandler(
            IProjectRepository projectRepository,
            IInstanceRepository instanceRepository,
            IGroupRepository groupRepository
            )
        {
            _projectRepository = projectRepository;
            _instanceRepository = instanceRepository;
            _groupRepository = groupRepository;
        }

        public async Task Handle(AssignGroup request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndGroupsByIdAsync(request.project_id)
              ?? throw new ProjectNotFoundException(request.project_id);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            if(instance.InstanceGroups.Any(g => g.Group.Name == request.dto.GroupName))
                throw new GroupWithNameAlreadyAssignedToInstanceException(request.instance_id, request.dto.GroupName);
            var group = await _groupRepository.GetGroupByNameAsync(request.dto.GroupName)
                ?? throw new GroupWithNameNotFoundException(request.dto.GroupName);
            var instanceGroup = new InstanceGroup
            {
                Group = group,
                InstanceId = instance.Id
            };
            instance.InstanceGroups.Add(instanceGroup);
            await _instanceRepository.UpdateInstanceAsync(instance);
        }
    }
}
