using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetProjectVersionsAsDtosHandler : IRequestHandler<GetProjectVersionsAsDtos, List<GetProjectVersionDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public GetProjectVersionsAsDtosHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task<List<GetProjectVersionDto>> Handle(GetProjectVersionsAsDtos request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);

            var versions = project.ProjectVersions
                .Select(pv =>
                new GetProjectVersionDto(
                        pv.Id,
                        _converter.VersionToVersionString(pv.Major, pv.Minor,pv.Patch),
                        pv.Description
                    )).ToList();

            return versions;
        }
    }
}
