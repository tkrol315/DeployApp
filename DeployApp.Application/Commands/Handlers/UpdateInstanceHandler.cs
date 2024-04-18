using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Application.Utils;
using DeployApp.Domain.Entities;
using DeployApp.Domain.Enums;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class UpdateInstanceHandler : IRequestHandler<UpdateInstance>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IInstanceRepository _instanceRepository;

        public UpdateInstanceHandler(IProjectRepository projectRepository, IInstanceRepository instanceRepository)
        {
            _projectRepository = projectRepository;
            _instanceRepository = instanceRepository;
        }

        public async Task Handle(UpdateInstance request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithInstancesAndProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var instance = project.Instances.FirstOrDefault(i => i.Id == request.instance_id)
                ?? throw new InstanceNotFoundException(request.instance_id);
            if (instance.Name != request.dto.Name)
                if (await _instanceRepository.InstanceWithNameAlreadyExists(request.project_id, request.dto.Name))
                    throw new ProjectAlreadyContainsInstanceWithNameException(request.project_id, request.dto.Name);
           
            if (!string.IsNullOrEmpty(request.dto.VersionString))
            {
                var dic = ProjectVersionConverter.VersionStringToDictionary(request.dto.VersionString);
                var existingVersion = project.ProjectVersions.FirstOrDefault(v => 
                    v.Major == dic[VersionParts.Major] &&
                    v.Minor == dic[VersionParts.Minor] && 
                    v.Patch == dic[VersionParts.Patch]);

                if (existingVersion != null)
                    instance.ProjectVersionId = existingVersion.Id;
                else if (!string.IsNullOrEmpty(request.dto.VersionDescription)) {
                    var newVersion = new ProjectVersion()
                    {
                        Major = dic[VersionParts.Major],
                        Minor = dic[VersionParts.Minor],
                        Patch = dic[VersionParts.Patch],
                        Description = request.dto.VersionDescription
                    };
                    project.ProjectVersions.Add(newVersion);
                    instance.ProjectVersion = newVersion;
                        }
                else
                    throw new VersionDescriptionMissingException(request.dto.VersionString);
                
            }
            instance.Type.Description = request.dto.TypeDescription;
            instance.Name = request.dto.Name;
            instance.Key = request.dto.Key;
            instance.Secret = request.dto.Secret;

            await _projectRepository.UpdateProjectAsync(project);
        }
    }
}
