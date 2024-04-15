using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Queries.Handlers
{
    public class GetTagAsDtoHandler : IRequestHandler<GetTagAsDto, GetTagDto>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagAsDtoHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<GetTagDto> Handle(GetTagAsDto request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetTagByIdAsync(request.tag_id)
                ?? throw new TagNotFoundException(request.tag_id);
            return new GetTagDto(tag.Id,tag.Name,tag.Description);
        }
    }
}
