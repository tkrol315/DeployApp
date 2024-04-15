using DeployApp.Application.Dtos;
using DeployApp.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetGroupsAsDtosHandler : IRequestHandler<GetGroupsAsDtos, List<GetGroupDto>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsAsDtosHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<GetGroupDto>> Handle(GetGroupsAsDtos request, CancellationToken cancellationToken)
        {
            var groups = _groupRepository.GetGroupsAsIQueryable();
            return await groups.Select(g => new GetGroupDto(g.Id, g.Name, g.Description)).ToListAsync();
        }
    }
}
