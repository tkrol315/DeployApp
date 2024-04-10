using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateProjectHandler : IRequestHandler<CreateProject, int>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(CreateProject request, CancellationToken cancellationToken)
        {
            if (await _projectRepository.ProjectWithTitleAlreadyExistsAsync(request.dto.Title))
                throw new ProjectWithNameAlreadyExistsException(request.dto.Title);
            var project = new Project()
            {
                Title = request.dto.Title,
                Description = request.dto.Description,
                IsActive = request.dto.IsActive,
                YtCode = request.dto.YtCode,
                RepositoryUrl = request.dto.RepositoryUrl,
                Instances = new List<Instance>(),
                ProjectVersions = new List<ProjectVersion>()
            };
            await _projectRepository.CreateProjectAsync(project);
            return project.Id;
        }
    }
}
