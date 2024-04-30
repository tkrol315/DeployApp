using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using FluentAssertions;
using Moq;
using DeployApp.Domain.Entities;

namespace DeployApp.UnitTests.CreateCommandHandlers
{
    public class CreateProjectHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        
        [Fact]
        public async Task Handle_Create_Project_Returns_New_Project_Id()
        {
            _projectRepositoryMock.Setup(p => p.ProjectWithTitleAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _projectRepositoryMock.Setup(p => p.CreateProjectAsync(It.IsAny<Project>())).ReturnsAsync(1);

            var handler = new CreateProjectHandler(_projectRepositoryMock.Object);
            var project = new CreateProjectDto("test", "test", true, "test", "test");
            var command = new CreateProject(project);

            var id = await handler.Handle(command, CancellationToken.None);

            id.Should().Be(1);
            _projectRepositoryMock.Verify(x => x.ProjectWithTitleAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.CreateProjectAsync(It.IsAny<Project>()), Times.Once);

        }

        [Fact]
        public async Task Handle_Create_Project_Throws_Project_With_Title_Already_Exists_Exception()
        {
            _projectRepositoryMock.Setup(p => p.ProjectWithTitleAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new CreateProjectHandler(_projectRepositoryMock.Object);
            var project = new CreateProjectDto("test", "test", true, "test", "test");
            var command = new CreateProject(project);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectWithTitleAlreadyExistsException>();
            _projectRepositoryMock.Verify(x => x.ProjectWithTitleAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.CreateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
