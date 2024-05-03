using DeployApp.Application.Abstractions;
using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.CreateCommandHandlers
{
    public class CreateDeployHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConverterMock = new();
        
        [Fact]
        public async Task Handle_Create_Deploy_Returns_New_Deploy_Id()
        {
            var project = new Project() { 
                Deploys = new List<Deploy>(),
                ProjectVersions = new List<ProjectVersion>() 
                {
                    new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                } 
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysAndProjectVersionsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _projectVersionConverterMock.Setup(c => c.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns("1.0.0");

            var dto = new CreateDeployDto("1.0.0", It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<bool>());
            var command = new CreateDeploy(It.IsAny<int>(), dto);
            var handler = new CreateDeployHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);

            var id = await handler.Handle(command, CancellationToken.None);

            id.Should().Be(It.IsAny<int>());
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(project), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task Handle_Create_Deploy_Throws_Project_Not_Found_Exception()
        {
          
            var dto = new CreateDeployDto("1.0.0", It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<bool>());
            var command = new CreateDeploy(It.IsAny<int>(), dto);
            var handler = new CreateDeployHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
            _projectVersionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Create_Deploy_Throws_Project_Version_Not_Found_Exception()
        {
            var project = new Project()
            {
                Deploys = new List<Deploy>(),
                ProjectVersions = new List<ProjectVersion>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysAndProjectVersionsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);

            var dto = new CreateDeployDto("1.0.0", It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<bool>());
            var command = new CreateDeploy(It.IsAny<int>(), dto);
            var handler = new CreateDeployHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectVersionNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(project), Times.Never);
            _projectVersionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        }

    }
}
