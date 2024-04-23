using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetProjectVersionAsDtoHandler : IRequestHandler<GetProjectVersionAsDto, GetProjectVersionDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectVersionConverter _converter;

        public GetProjectVersionAsDtoHandler(IProjectRepository projectRepository, IProjectVersionConverter converter)
        {
            _projectRepository = projectRepository;
            _converter = converter;
        }

        public async Task<GetProjectVersionDto> Handle(GetProjectVersionAsDto request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithProjectVersionsByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            var projectVersion = project.ProjectVersions.FirstOrDefault(pv => pv.Id == request.version_id)
                ?? throw new ProjectVersionNotFoundException(request.version_id);
            return new GetProjectVersionDto(projectVersion.Id, 
                _converter.VersionToVersionString(projectVersion.Major, projectVersion.Minor, projectVersion.Patch),
                projectVersion.Description);
        }
    }
}
