using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetInstancesAsDtosHandler : IRequestHandler<GetInstancesAsDtos, List<GetInstanceDto>>
    {
        private readonly IInstanceRepository _instanceRepository;

        public GetInstancesAsDtosHandler(IInstanceRepository instanceRepository)
        {
            _instanceRepository = instanceRepository;
        }

        public async Task<List<GetInstanceDto>> Handle(GetInstancesAsDtos request, CancellationToken cancellationToken)
        {
            var instances = _instanceRepository.GetAllAsIQueryable(request.projectId);
            var instanceDtos = instances.Select(i => new GetInstanceDto(
                i.Id,
                i.ProjectId,
                i.Type.Description,
                i.Key,
                i.Secret,
                i.InstanceTags.Select(it => new GetTagDto(
                    it.Tag.Id,
                    it.Tag.Name,
                    it.Tag.Description
                    )),
                i.InstanceGroups.Select(g => new GetGroupDto(
                    g.Group.Id,
                    g.Group.Name,
                    g.Group.Description
                    )),
                i.ProjectVersion == null ? null : new GetProjectVersionDto(
                        i.ProjectVersion.Major,
                        i.ProjectVersion.Minor,
                        i.ProjectVersion.Patch,
                        i.ProjectVersion.Description)
                ));
            

            if (!string.IsNullOrEmpty(request.searchPhrase.TagName))
                instanceDtos = instanceDtos.Where(i => i.Tags.Any(t => t.Name == request.searchPhrase.TagName));
            if(!string.IsNullOrEmpty(request.searchPhrase.GroupName))
                instanceDtos = instanceDtos.Where(i => i.Groups.Any(g => g.Name == request.searchPhrase.GroupName));
            if (!string.IsNullOrEmpty(request.searchPhrase.TypeDescription))
                instanceDtos = instanceDtos.Where(i => i.TypeDescription == request.searchPhrase.TypeDescription);
            if (!string.IsNullOrEmpty(request.searchPhrase.ActualVersion))
            {
                string[] arr = request.searchPhrase.ActualVersion.Split(".");
                if (arr.Length != 3)
                    throw new ProjectVersionFormatException(request.searchPhrase.ActualVersion);
                if (int.TryParse(arr[0], out var major) && int.TryParse(arr[1], out var minor) && int.TryParse(arr[2], out var patch))
                    instanceDtos = instanceDtos.Where(i => i.ProjectVersion.Major == major && i.ProjectVersion.Minor == minor && i.ProjectVersion.Patch == patch);
                else
                    throw new ProjectVersionParseException(request.searchPhrase.ActualVersion);
            }
            return instanceDtos.ToList();
           
        }
    }
}
