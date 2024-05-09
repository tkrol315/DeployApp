using DeployApp.Application.Abstractions;
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
    public class UpdateDeployHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _versionConverterMock = new();

        [Fact]
        public async Task Handle_Update_Deploy_Success()
        {
            var versionString = "1.0.0";
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new(){ Id = deployId },
                },
                ProjectVersions = new List<ProjectVersion>()
                {
                    new() {Major = 1,Minor = 0, Patch = 0}
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);
            _versionConverterMock.Setup(v => v.VersionToVersionString(1, 0, 0)).Returns(versionString);

            var dto = new UpdateDeployDto(versionString, It.IsAny<DateTime>(), It.IsAny<DateTime>(),It.IsAny<string>(), true);
            var command = new UpdateDeploy(project.Id, deployId, dto);
            var handler = new UpdateDeployHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(p => p.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(p => p.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Deploy_Throws_Project_Not_Found_Exception()
        {
            var dto = new UpdateDeployDto(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(),It.IsAny<string>(), true);
            var command = new UpdateDeploy(It.IsAny<int>(), It.IsAny<int>(), dto);
            var handler = new UpdateDeployHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(p => p.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _projectRepositoryMock.Verify(p => p.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Deploy_Throws_Deploy_Not_Found_Exception()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);

            var dto = new UpdateDeployDto(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(),It.IsAny<string>(), true);
            var command = new UpdateDeploy(project.Id, deployId, dto);
            var handler = new UpdateDeployHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(p => p.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _projectRepositoryMock.Verify(p => p.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Deploy_Throws_Project_Version_Not_Found_Exception()
        {
            var versionString = "1.0.0";
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new(){ Id = deployId },
                },
                ProjectVersions = new List<ProjectVersion>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);
            _versionConverterMock.Setup(v => v.VersionToVersionString(1, 0, 0)).Returns(versionString);

            var dto = new UpdateDeployDto(versionString, It.IsAny<DateTime>(), It.IsAny<DateTime>(),It.IsAny<string>(), true);
            var command = new UpdateDeploy(project.Id, deployId, dto);
            var handler = new UpdateDeployHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectVersionNotFoundException>();
            _projectRepositoryMock.Verify(p => p.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _projectRepositoryMock.Verify(p => p.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
