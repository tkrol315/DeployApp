﻿using DeployApp.Application.Repositories;
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
            var tag = new Tag()
            {
                Name = request.createTagDto.Name,
                Description = request.createTagDto.Description,
            };
            return await _tagRepository.AddTagAsync(tag);
        }
    }
}
