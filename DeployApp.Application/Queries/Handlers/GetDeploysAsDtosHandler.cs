using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetDeploysAsDtosHandler : IRequestHandler<GetDeploysAsDtos, List<GetDeployDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public GetDeploysAsDtosHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task<List<GetDeployDto>> Handle(GetDeploysAsDtos request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithDeploysByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var dtos = project.Deploys.Select(d => new GetDeployDto(
                d.Id,
                d.ProjectId,
                d.ProjectVersionId,
                _converter.VersionToVersionString(d.ProjectVersion.Major, d.ProjectVersion.Minor, d.ProjectVersion.Patch),
                d.Start,
                d.End,
                d.IsActive)).ToList();
            return dtos;
        }
    }
}
