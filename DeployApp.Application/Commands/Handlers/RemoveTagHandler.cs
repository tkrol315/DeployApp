using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class RemoveTagHandler : IRequestHandler<RemoveTag>
    {
        private readonly ITagRepository _tagRepository;

        public RemoveTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Handle(RemoveTag request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetTagByIdAsync(request.id)
                ?? throw new TagNotFoundException(request.id);
            await _tagRepository.RemoveTagAsync(tag);
        }
    }
}
