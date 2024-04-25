using DeployApp.Application.Abstractions;
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
        private readonly ITransactionHandler _transactionHandler;

        public AssignTagHandler(
            IProjectRepository projectRepository,
            ITagRepository tagRepository,
            IInstanceRepository instanceRepository,
            IMediator mediator,
            ITransactionHandler transactionHandler
            )
        {
            _projectRepository = projectRepository;
            _tagRepository = tagRepository;
            _instanceRepository = instanceRepository;
            _mediator = mediator;
            _transactionHandler = transactionHandler;
        }

        public async Task Handle(AssignTag request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndTagsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            var dtoNameToUpper = request.dto.Name.ToUpper();
            if (instance.InstanceTags.Any(it => it.Tag.Name == dtoNameToUpper))
                throw new TagWithNameAlreadyAssignedToInstanceException(request.instance_id, dtoNameToUpper);
            var tag = await _tagRepository.GetTagByNameAsync(dtoNameToUpper);
            var instanceTag = new InstanceTag();

            using var transaction = _transactionHandler.BeginTransaction();
            try
            {
                instanceTag.Instance = instance;
                if (tag is null)
                {
                    var command = new CreateTag(new CreateTagDto(dtoNameToUpper, request.dto.Description));
                    var id = await _mediator.Send(command);
                    instanceTag.TagId = id;
                }
                else
                    instanceTag.Tag = tag;

                instance.InstanceTags.Add(instanceTag);
                await _instanceRepository.UpdateInstanceAsync(instance);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception();
            }
        }
    }
}
