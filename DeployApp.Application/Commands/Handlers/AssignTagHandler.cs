using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class AssignTagHandler : IRequestHandler<AssignTag>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IInstanceRepository _instanceRepository;
        private readonly IMediator _mediator;

        public AssignTagHandler(
            IProjectRepository projectRepository,
            ITagRepository tagRepository,
            IInstanceRepository instanceRepository,
            IMediator mediator
            )
        {
            _projectRepository = projectRepository;
            _tagRepository = tagRepository;
            _instanceRepository = instanceRepository;
            _mediator = mediator;
        }

        public async Task Handle(AssignTag request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndTagsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            if (instance.InstanceTags.Any(it => it.Tag.Name == request.dto.Name))
                throw new TagWithNameAlreadyAssignedToInstanceException(request.instance_id, request.dto.Name);
            var tag = await _tagRepository.GetTagByNameAsync(request.dto.Name);
            var instanceTag = new InstanceTag();
            instanceTag.Instance = instance;
            if (tag is null)
            {
                var command = new CreateTag(new CreateTagDto(request.dto.Name, request.dto.Description));
                var id = await _mediator.Send(command);               
                instanceTag.TagId = id;
            }
            else
                instanceTag.Tag = tag;
          
            instance.InstanceTags.Add(instanceTag);
            await _instanceRepository.UpdateInstanceAsync(instance);
        }
    }
}
