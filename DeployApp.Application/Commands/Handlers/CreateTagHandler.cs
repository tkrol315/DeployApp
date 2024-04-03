using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateTagHandler : IRequestHandler<CreateTag>
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task Handle(CreateTag request, CancellationToken cancellationToken)
        {
            var tag = new Tag()
            {
                Name = request.createTagDto.Name,
                Description = request.createTagDto.Description,
            };
            await _tagRepository.AddTagAsync(tag);
        }
    }
}
