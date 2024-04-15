using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetGroupAsDtoHandler : IRequestHandler<GetGroupAsDto, GetGroupDto>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupAsDtoHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<GetGroupDto> Handle(GetGroupAsDto request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.group_id)
                ?? throw new GroupNotFoundException(request.group_id);
            return new GetGroupDto(group.Id, group.Name, group.Description);
        }
    }
}
