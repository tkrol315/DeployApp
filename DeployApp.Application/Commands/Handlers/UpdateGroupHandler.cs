using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class UpdateGroupHandler : IRequestHandler<UpdateGroup>
    {
        private readonly IGroupRepository _groupRepository;

        public UpdateGroupHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task Handle(UpdateGroup request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.group_id)
                ?? throw new GroupNotFoundException(request.group_id);
            if (group.Name != request.dto.Name)
                if (await _groupRepository.GroupWithNameAlreadyExistsAsync(request.dto.Name))
                    throw new GroupWithNameAlreadyExistsException(request.dto.Name);
            group.Name = request.dto.Name;
            group.Description = request.dto.Description;
            await _groupRepository.UpdateGroupAsync(group);
        }
    }
}
