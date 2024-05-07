using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.RemoveCommandHandlers
{
    public class RemoveDeployHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        [Fact]
        public async Task Handle_Remove_Deploy_Success()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new()
                    {
                        Id = deployId
                    }
                }
            };
            _projectRepositoryMock.Setup(x => x.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);
            var command = new RemoveDeploy(project.Id, deployId);
            var handler = new RemoveDeployHandler(_projectRepositoryMock.Object);
            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Remove_Deploy_Throws_Project_Not_Found_Exception()
        {
            var command = new RemoveDeploy(It.IsAny<int>(), It.IsAny<int>());
            var handler = new RemoveDeployHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }


        [Fact]
        public async Task Handle_Remove_Deploy_Throws_Deploy_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };
            _projectRepositoryMock.Setup(x => x.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);
            var command = new RemoveDeploy(project.Id, It.IsAny<int>());
            var handler = new RemoveDeployHandler(_projectRepositoryMock.Object);
            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
