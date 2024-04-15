using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateGroupHandler : IRequestHandler<CreateGroup, int>
    {
        private readonly IGroupRepository _groupRepository;

        public CreateGroupHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<int> Handle(CreateGroup request, CancellationToken cancellationToken)
        {
            if (await _groupRepository.GroupWithNameAlreadyExistsAsync(request.dto.Name))
                throw new GroupWithNameAlreadyExistsException(request.dto.Name);
            var group = new Group()
            {
                Name = request.dto.Name,
                Description = request.dto.Description,
                InstanceGroups = new List<InstanceGroup>()
            };
            return await _groupRepository.CreateGroupAsync(group);
        }
    }
}
