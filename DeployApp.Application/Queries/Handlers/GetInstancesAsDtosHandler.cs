using DeployApp.Application.Dtos;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetInstancesAsDtosHandler : IRequestHandler<GetInstancesAsDtos, List<GetInstanceDto>>
    {
        private readonly IInstanceRepository _instanceRepository;
        //To change
        public async Task<List<GetInstanceDto>> Handle(GetInstancesAsDtos request, CancellationToken cancellationToken)
        {
            var instances = _instanceRepository.GetAll(request.projectId);
            var instanceDtos = instances.Select(i =>
                new GetInstanceDto(i.Id,
                i.ProjectId,
                i.Type.Description,
                i.Key,
                i.Secret,
                i.InstanceTags,
                i.InstanceGroups,
                i.ProjectVersion.Major,
                i.ProjectVersion.Minor,
                i.ProjectVersion.Patch
            ));
            if (!string.IsNullOrEmpty(request.searchPhrase.TagName))
                instanceDtos = instanceDtos.Where(i => i.InstanceTags.Any(it => it.Tag.Name == request.searchPhrase.TagName));
            if(!string.IsNullOrEmpty(request.searchPhrase.GroupName))
                instanceDtos = instanceDtos.Where(i => i.InstanceGroups.Any(ig => ig.Group.Name == request.searchPhrase.GroupName));
            if (!string.IsNullOrEmpty(request.searchPhrase.TypeDescription))
                instanceDtos = instanceDtos.Where(i => i.TypeDescription == request.searchPhrase.TypeDescription);

            return instanceDtos.ToList();
           
        }
        //===
    }
}
