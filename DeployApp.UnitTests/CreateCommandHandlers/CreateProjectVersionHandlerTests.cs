using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Commands;
using DeployApp.Application.Dtos;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using Moq;
using DeployApp.Application.Abstractions;
using FluentAssertions;
using DeployApp.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using DeployApp.Application.Exceptions;

namespace DeployApp.UnitTests.CreateCommandHandlers
{
    public class CreateProjectVersionHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConverterMock = new();

        [Fact]
        public async Task Handle_Create_Project_Version_Returns_New_Project_Version_Id()
        {
            var project = new Project() { Id = 1, ProjectVersions = new List<ProjectVersion>()};
            var versionDictionary = new Dictionary<VersionParts, int>() { { VersionParts.Major,1}, { VersionParts.Minor, 0 }, {VersionParts.Patch ,0 } };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _projectRepositoryMock.Setup(p => p.UpdateProjectAsync(It.IsAny<Project>()));
            _projectVersionConverterMock.Setup(c => c.VersionStringToDictionary(It.IsAny<string>())).Returns(versionDictionary);

            var handler = new CreateProjectVersionHandler(_projectRepositoryMock.Object,_projectVersionConverterMock.Object);
            var projectVersion = new CreateProjectVersionDto("1.0.0", "test");
            var command = new CreateProjectVersion(It.IsAny<int>(),projectVersion);

            var id = await handler.Handle(command, CancellationToken.None);

            id.Should().Be(It.IsAny<int>());
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
            

        }

        [Fact]
        public async Task Handle_Create_Project_Version_Throws_Project_Not_Found_Exception()
        {

            var handler = new CreateProjectVersionHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);
            var command = new CreateProjectVersion(It.IsAny<int>(), It.IsAny<CreateProjectVersionDto>());

            var act = ()=>  handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Create_Project_Version_Throws_Project_Version_Format_Exception()
        {
            var project = new Project() { Id = 1, ProjectVersions = new List<ProjectVersion>() };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _projectVersionConverterMock.Setup(c => c.VersionStringToDictionary(It.IsAny<string>()))
                .Throws(new ProjectVersionFormatException(It.IsAny<string>()));

            var handler = new CreateProjectVersionHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);
            var command = new CreateProjectVersion(It.IsAny<int>(), new CreateProjectVersionDto(It.IsAny<string>(),It.IsAny<string>()));

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectVersionFormatException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Create_Project_Version_Throws_Project_Version_Parse_Exception()
        {
            var project = new Project() { Id = 1, ProjectVersions = new List<ProjectVersion>() };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _projectVersionConverterMock.Setup(c => c.VersionStringToDictionary(It.IsAny<string>()))
                .Throws(new ProjectVersionParseException(It.IsAny<string>()));

            var handler = new CreateProjectVersionHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);
            var command = new CreateProjectVersion(It.IsAny<int>(), new CreateProjectVersionDto(It.IsAny<string>(), It.IsAny<string>()));

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectVersionParseException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

    }
}
