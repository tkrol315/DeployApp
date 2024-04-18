using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Application.Utils;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetProjectVersionAsDtoHandler : IRequestHandler<GetProjectVersionAsDto, GetProjectVersionDto>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectVersionAsDtoHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetProjectVersionDto> Handle(GetProjectVersionAsDto request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var projectVersion = project.ProjectVersions.FirstOrDefault(pv => pv.Id == request.version_id)
                ?? throw new ProjectVersionNotFoundException(request.version_id);
            return new GetProjectVersionDto(projectVersion.Id, 
                ProjectVersionConverter.VersionToVersionString(projectVersion.Major, projectVersion.Minor, projectVersion.Patch),
                projectVersion.Description);
        }
    }
}
