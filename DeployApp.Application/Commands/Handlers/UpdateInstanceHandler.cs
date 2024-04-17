using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
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
            //not finished 
            if (request.dto.VersionId != null)
            {

                instance.ProjectVersionId = request.dto.VersionId;

            }

            instance.Type.Description = request.dto.TypeDescription;
            instance.Name = request.dto.Name;
            instance.Key = request.dto.Key;
            instance.Secret = request.dto.Secret;
            
           

            
        }
    }
}
