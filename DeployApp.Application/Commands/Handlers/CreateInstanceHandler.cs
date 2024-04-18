using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Application.Utils;
using DeployApp.Domain.Entities;
using DeployApp.Domain.Enums;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateInstanceHandler : IRequestHandler<CreateInstance, int>
    {
        private readonly IInstanceRepository _instanceRepository;
        private readonly IProjectRepository _projectRepository;
        public CreateInstanceHandler(IInstanceRepository instanceRepository, IProjectRepository projectRepository)
        {
            _instanceRepository = instanceRepository;
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateInstance request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            if (await _instanceRepository.InstanceWithNameAlreadyExists(request.project_id, request.dto.Name))
                throw new ProjectAlreadyContainsInstanceWithNameException(request.project_id, request.dto.Name);

            var instance = new Instance()
            {
                Type = new Domain.Entities.Type() { Description = request.dto.TypeDescription },
                Name = request.dto.Name,
                Key = request.dto.Key,
                Secret = request.dto.Secret,
                InstanceTags = new List<InstanceTag>(),
                InstanceGroups = new List<InstanceGroup>()
            };
            if (!string.IsNullOrEmpty(request.dto.VersionString))
            {
                var versionDic = ProjectVersionConverter.VersionStringToDictionary(request.dto.VersionString);
                var existingVersion = project.ProjectVersions.FirstOrDefault(v =>
                   v.Major == versionDic[VersionParts.Major] &&
                   v.Minor == versionDic[VersionParts.Minor] &&
                   v.Patch == versionDic[VersionParts.Patch]);
                if (existingVersion != null)
                    instance.ProjectVersionId = existingVersion.Id;
                else if (!string.IsNullOrEmpty(request.dto.VersionDescription))
                {
                    var newVersion = new ProjectVersion()
                    {
                        Major = versionDic[VersionParts.Major],
                        Minor = versionDic[VersionParts.Minor],
                        Patch = versionDic[VersionParts.Patch],
                        Description = string.IsNullOrEmpty(request.dto.VersionDescription) ? null : request.dto.VersionDescription
                    };
                    project.ProjectVersions.Add(newVersion);
                    instance.ProjectVersion = newVersion;
                }
                else
                    throw new VersionDescriptionMissingException(request.dto.VersionString);
            }
            project.Instances.Add(instance);

            return await _instanceRepository.CreateInstanceAsync(instance);
        }
    }


}
