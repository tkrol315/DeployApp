using DeployApp.Application.Dtos;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetTagsAsDtosHandler : IRequestHandler<GetTagsAsDtos, List<GetTagDto>>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagsAsDtosHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<List<GetTagDto>> Handle(GetTagsAsDtos request, CancellationToken cancellationToken)
        {
            var tags = await _tagRepository.GetTagsAsync();
            return tags.Select(t => new GetTagDto(t.Id, t.Name, t.Description)).ToList();
        }
    }
}
