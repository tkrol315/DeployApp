using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetProjectVersionsAsDtosHandler : IRequestHandler<GetProjectVersionsAsDtos, List<GetProjectVersionDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectVersionsAsDtosHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<GetProjectVersionDto>> Handle(GetProjectVersionsAsDtos request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);

            var versions = project.ProjectVersions
                .Select(pv =>
                new GetProjectVersionDto(
                        pv.Id,
                        string.Join(".", pv.Major, pv.Minor, pv.Patch),
                        pv.Description
                    )).ToList();

            return versions;
        }
    }
}
