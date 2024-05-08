using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.UpdateCommandHandlers
{
    public class UpdateProjectHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        [Fact]
        public async Task Handle_Update_Project_Success()
        {
            var project = new Project() { Id = 1, Title = "test"};
            _projectRepositoryMock.Setup(p => p.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);
            _projectRepositoryMock.Setup(p => p.ProjectWithTitleAlreadyExistsAsync(project.Title)).ReturnsAsync(false);

            var dto = new UpdateProjectDto("newTitle", "newDescription", true, "test", "test");
            var command = new UpdateProject(project.Id, dto);
            var handler = new UpdateProjectHandler(_projectRepositoryMock.Object);

            await handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.ProjectWithTitleAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Project_Throws_Project_Not_Found_Exception()
        {

            var dto = new UpdateProjectDto("newTitle", "newDescription", true, "test", "test");
            var command = new UpdateProject(It.IsAny<int>(), dto);
            var handler = new UpdateProjectHandler(_projectRepositoryMock.Object);

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.ProjectWithTitleAlreadyExistsAsync(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Project_Throws_Project_With_Title_Already_Exists_Exception()
        {
            var project = new Project() { Id = 1, Title = "test" };
            _projectRepositoryMock.Setup(p => p.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);
            _projectRepositoryMock.Setup(p => p.ProjectWithTitleAlreadyExistsAsync("newTitle")).ReturnsAsync(true);

            var dto = new UpdateProjectDto("newTitle", "newDescription", true, "test", "test");
            var command = new UpdateProject(project.Id, dto);
            var handler = new UpdateProjectHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectWithTitleAlreadyExistsException>();
            _projectRepositoryMock.Verify(x => x.GetProjectByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.ProjectWithTitleAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
