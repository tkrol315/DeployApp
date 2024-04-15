using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class RemoveGroupHandler : IRequestHandler<RemoveGroup>
    {
        private readonly IGroupRepository _groupRepository;

        public RemoveGroupHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task Handle(RemoveGroup request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.group_id)
                ?? throw new GroupNotFoundException(request.group_id);
            await _groupRepository.RemoveGroupAsync(group);
        }
    }
}
