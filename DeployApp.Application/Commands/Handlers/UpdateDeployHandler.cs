using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries;
using DeployApp.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace DeployApp.Application.Commands.Handlers
{
    public class UpdateDeployHandler : IRequestHandler<UpdateDeploy>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public UpdateDeployHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task Handle(UpdateDeploy request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithDeploysByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var deploy = project.Deploys.FirstOrDefault(d => d.Id == request.deploy_id)
                ?? throw new DeployNotFoundException(request.deploy_id);
            var projectVersion = project.ProjectVersions.FirstOrDefault(v => _converter.VersionToVersionString(v.Major,v.Minor,v.Patch) == request.dto.VersionString)
                ?? throw new ProjectVersionNotFoundException(request.dto.VersionString);
            deploy.ProjectVersion = projectVersion;
            deploy.Start = request.dto.Start;
            deploy.End = request.dto.End;
            deploy.Description = request.dto.Description;
            deploy.IsActive = request.dto.IsActive;
            await _projectRepository.UpdateProjectAsync(project);
        }
    }
}
