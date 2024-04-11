using DeployApp.Application.Dtos;
using DeployApp.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetProjectsAsDtosHandler : IRequestHandler<GetProjectsAsDtos, List<GetProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectsAsDtosHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<GetProjectDto>> Handle(GetProjectsAsDtos request, CancellationToken cancellationToken)
        {
            var projects = _projectRepository.GetProjectsAsIQueryable();
            var projectDtos = await projects.Select(p => new GetProjectDto(p.Id, p.Title, p.Description, p.IsActive,
                p.YtCode, p.RepositoryUrl)).ToListAsync();
            return projectDtos;
        }
    }
}
