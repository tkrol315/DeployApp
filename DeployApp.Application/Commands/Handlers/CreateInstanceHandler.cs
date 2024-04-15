using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
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
            if (!await _projectRepository.ProjectWithIdAlredyExistsAsync(request.project_id))
                throw new ProjectNotFoundException(request.project_id);

            var instance = new Instance()
            {
                ProjectId = request.project_id,
                Type = new Domain.Entities.Type() { Description = request.dto.TypeDescription},
                Key = request.dto.Key,
                Secret = request.dto.Secret,
                ProjectVersionId = request.dto.ProjectVersionId ?? null,
                InstanceTags = new List<InstanceTag>(),
                InstanceGroups = new List<InstanceGroup>()
            };
            
           return await _instanceRepository.CreateInstanceAsync(instance);
        }
    }
  

}
