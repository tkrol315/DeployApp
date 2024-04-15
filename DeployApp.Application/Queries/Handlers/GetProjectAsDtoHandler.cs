using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetProjectAsDtoHandler : IRequestHandler<GetProjectAsDto, GetProjectDto>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectAsDtoHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<GetProjectDto> Handle(GetProjectAsDto request, CancellationToken cancellationToken)
        {
            var p = await _projectRepository.GetProjectByIdAsync(request.project_id)
                ?? throw new ProjectNotFoundException(request.project_id);
            return new GetProjectDto(p.Id, p.Title, p.Description, p.IsActive, p.YtCode,
                p.RepositoryUrl);
        }
    }
}
