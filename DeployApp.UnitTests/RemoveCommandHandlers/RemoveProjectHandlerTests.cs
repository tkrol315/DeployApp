using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Equivalency.Tracing;
using Moq;
using System.Runtime.CompilerServices;

namespace DeployApp.UnitTests.RemoveCommandHandlers
{
    public class RemoveProjectHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        [Fact]
        public async Task Handle_Remove_Project_Success()
        {
            var project = new Project() { Id = 1 };
            _projectRepositoryMock.Setup(x => x.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveProject(project.Id);
            var handler = new RemoveProjectHandler(_projectRepositoryMock.Object);
            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.RemoveProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Remove_Project_Throws_Project_Not_Found_Exception()
        {
            
            var command = new RemoveProject(It.IsAny<int>());
            var handler = new RemoveProjectHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync <ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.RemoveProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
