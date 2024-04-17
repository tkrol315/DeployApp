using DeployApp.Application.Dtos;
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
        private readonly IMediator _mediator;

        public AssignGroupHandler(
            IProjectRepository projectRepository,
            IInstanceRepository instanceRepository,
            IGroupRepository groupRepository,
            IMediator mediator
            )
        {
            _projectRepository = projectRepository;
            _instanceRepository = instanceRepository;
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        public async Task Handle(AssignGroup request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndGroupsByIdAsync(request.project_id)
              ?? throw new ProjectNotFoundException(request.project_id);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            if(instance.InstanceGroups.Any(g => g.Group.Name == request.dto.Name))
                throw new GroupWithNameAlreadyAssignedToInstanceException(request.instance_id, request.dto.Name);
            var group = await _groupRepository.GetGroupByNameAsync(request.dto.Name);
            var instanceGroup = new InstanceGroup();
            instanceGroup.Instance = instance;
            if(group is null)
            {
                var command = new CreateGroup(new CreateGroupDto(request.dto.Name, request.dto.Description));
                var id = await _mediator.Send(command);
   
                instanceGroup.GroupId = id;
            }
            else
                instanceGroup.Group = group;
            
            
            instance.InstanceGroups.Add(instanceGroup);
            await _instanceRepository.UpdateInstanceAsync(instance);
        }
    }
}
