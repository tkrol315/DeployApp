using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class CreateTagHandler : IRequestHandler<CreateTag,int>
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<int> Handle(CreateTag request, CancellationToken cancellationToken)
        {
            if (await _tagRepository.TagWithNameAlreadyExistsAsync(request.createTagDto.Name.ToUpper()))
                throw new TagWithNameAlreadyExistsException(request.createTagDto.Name.ToUpper());
            var tag = new Tag()
            {
                Name = request.createTagDto.Name.ToUpper(),
                Description = request.createTagDto.Description,
                InstanceTags = new List<InstanceTag>()
            };
            return await _tagRepository.AddTagAsync(tag);
        }
    }
}
