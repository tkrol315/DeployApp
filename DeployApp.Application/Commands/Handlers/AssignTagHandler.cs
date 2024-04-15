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

        public AssignTagHandler(
            IProjectRepository projectRepository,
            ITagRepository tagRepository,
            IInstanceRepository instanceRepository
            )
        {
            _projectRepository = projectRepository;
            _tagRepository = tagRepository;
            _instanceRepository = instanceRepository;
        }

        public async Task Handle(AssignTag request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndTagsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            if (instance.InstanceTags.Any(it => it.Tag.Name == request.dto.TagName))
                throw new TagWithNameAlreadyAssignedToInstanceException(request.instance_id, request.dto.TagName);
            var tag = await _tagRepository.GetTagByNameAsync(request.dto.TagName)
                ?? throw new TagWithNameNotFoundException(request.dto.TagName);

            var instanceTag = new InstanceTag()
            {
                InstanceId = request.instance_id,
                Tag = tag
            };
            instance.InstanceTags.Add(instanceTag);
            await _instanceRepository.UpdateInstanceAsync(instance);
        }
    }
}
