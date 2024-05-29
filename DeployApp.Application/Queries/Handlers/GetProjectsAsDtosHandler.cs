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
            if (!string.IsNullOrEmpty(request.searchPhrase.Title))
                projects = projects.Where(p => 
                    p.Title.Contains(request.searchPhrase.Title));
            if(!string.IsNullOrEmpty(request.searchPhrase.Description))
                projects = projects.Where(p =>
                    p.Description.Contains(request.searchPhrase.Description));
            if (request.searchPhrase.isActive.HasValue)
                projects = projects.Where(p =>
                    p.IsActive == request.searchPhrase.isActive.Value);

            var projectDtos = await projects.Select(p => new GetProjectDto(p.Id, p.Title, p.Description, p.IsActive,
                p.YtCode, p.RepositoryUrl)).ToListAsync();
            return projectDtos;
        }
    }
}
