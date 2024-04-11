using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class AssignTagToInstanceHandler : IRequestHandler<AssignTagToInstance>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IInstanceRepository _instanceRepository;

        public AssignTagToInstanceHandler(
            IProjectRepository projectRepository,
            ITagRepository tagRepository,
            IInstanceRepository instanceRepository
            )
        {
            _projectRepository = projectRepository;
            _tagRepository = tagRepository;
            _instanceRepository = instanceRepository;
        }

        public async Task Handle(AssignTagToInstance request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesByIdAsync(request.projectId)
                ?? throw new ProjectNotFoundException(request.projectId);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instanceId)
                ?? throw new InstanceNotFoundException(request.instanceId);
            if (instance.InstanceTags.Any(it => it.TagId == request.tagId))
                throw new TagAlreadyAssignedToInstance(request.instanceId, request.tagId);
            if (!await _tagRepository.TagExistsByIdAsync(request.tagId))
                throw new TagNotFoundException(request.tagId);

            var instanceTag = new InstanceTag()
            {
                InstanceId = request.instanceId,
                TagId = request.tagId,
            };
            instance.InstanceTags.Add(instanceTag);
            await _instanceRepository.UpdateInstanceAsync(instance);
        }
    }
}
