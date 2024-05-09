using DeployApp.Application.Abstractions;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateDeployHandler : IRequestHandler<CreateDeploy, int>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public CreateDeployHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task<int> Handle(CreateDeploy request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithDeploysAndProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var foundVersion = project.ProjectVersions.FirstOrDefault(pv =>
            _converter.VersionToVersionString(pv.Major, pv.Minor, pv.Patch) == request.dto.VersionString)
                ?? throw new ProjectVersionNotFoundException(request.dto.VersionString);

            var deploy = new Deploy()
            {
                Project = project,
                ProjectVersion = foundVersion,
                Start = request.dto.Start,
                End = request.dto.End,
                Description = request.dto.Description,
                IsActive = request.dto.IsActive,
                DeployInstances = new List<DeployInstance>()
            };

            project.Deploys.Add(deploy);
            await _projectRepository.UpdateProjectAsync(project);
            return deploy.Id;
        }
    }
}
