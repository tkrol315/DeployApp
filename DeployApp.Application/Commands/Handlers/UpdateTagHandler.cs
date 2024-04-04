using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class UpdateTagHandler : IRequestHandler<UpdateTag>
    {
        private readonly ITagRepository _tagRepository;

        public UpdateTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Handle(UpdateTag request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetTagByIdAsync(request.id)
                ?? throw new TagNotFoundException(request.id);
            tag.Description = request.updateTagDto.Description;
            await _tagRepository.UpdateTagAsync(tag);
        }
    }
}
