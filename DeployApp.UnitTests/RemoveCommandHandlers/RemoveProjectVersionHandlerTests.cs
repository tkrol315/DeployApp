using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.RemoveCommandHandlers
{
    public class RemoveProjectVersionHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        [Fact]
        public async Task Handle_Remove_Project_Version_Success()
        {
            var versionId = 1;
            var project = new Project()
            {
                Id = 1,
                ProjectVersions = new List<ProjectVersion>()
                {
                    new() { Id = versionId}
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveProjectVersion(project.Id, versionId);
            var handler = new RemoveProjectVersionHandler(_projectRepositoryMock.Object);
            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);

        }

        [Fact]
        public async Task Handle_Remove_Project_Version_Throws_Project_Not_Found_Exception()
        {

            var command = new RemoveProjectVersion(It.IsAny<int>(), It.IsAny<int>());
            var handler = new RemoveProjectVersionHandler(_projectRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Remove_Project_Version_Throws_Project_Version_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                ProjectVersions = new List<ProjectVersion>() 
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveProjectVersion(project.Id, It.IsAny<int>());
            var handler = new RemoveProjectVersionHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectVersionNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);

        }

    }
}
