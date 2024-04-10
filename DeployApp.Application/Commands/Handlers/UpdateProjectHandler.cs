﻿using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using MediatR;

namespace DeployApp.Application.Commands.Handlers
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProject>
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(UpdateProject request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.id)
                ?? throw new ProjectNotFoundException(request.id);
            if (project.Title != request.dto.Title)
                if (await _projectRepository.ProjectWithTitleAlreadyExistsAsync(request.dto.Title))
                    throw new ProjectWithNameAlreadyExistsException(request.dto.Title);
            project.Title = request.dto.Title;
            project.Description = request.dto.Description;
            project.IsActive = request.dto.IsActive;
            project.YtCode = request.dto.YtCode;
            project.RepositoryUrl = request.dto.RepositoryUrl;
            await _projectRepository.UpdateProjectAsync(project);
        }
    }
}