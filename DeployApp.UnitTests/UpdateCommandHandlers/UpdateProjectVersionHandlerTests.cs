using DeployApp.Application.Abstractions;
using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Domain.Enums;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.UpdateCommandHandlers
{
    public class UpdateProjectVersionHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConverterMock = new();

        [Fact]
        public async Task Handle_Update_Project_Version_Success()
        {
            var projectVersionId = 1;
            var project = new Project()
            {
                Id = 1,
                ProjectVersions = new List<ProjectVersion>()
                {
                    new()
                    {
                        Id = projectVersionId,
                        Major = 1,
                        Minor = 0,
                        Patch = 0,
                        Description = "test"
                    }
                }
            };
            var dic = new Dictionary<VersionParts, int>
            {
                {VersionParts.Major, 1},
                {VersionParts.Minor, 0},
                {VersionParts.Patch, 1},
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _projectVersionConverterMock.Setup(pv => pv.VersionStringToDictionary("1.0.1")).Returns(dic);

            var dto = new UpdateProjectVersionDto("1.0.1", "test");
            var command = new UpdateProjectVersion(project.Id, projectVersionId, dto);
            var handler = new UpdateProjectVersionHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);
            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Project_Version_Throws_Project_Not_Found_Exception()
        {
            
            var dto = new UpdateProjectVersionDto("1.0.1", "test");
            var command = new UpdateProjectVersion(It.IsAny<int>(), It.IsAny<int>(), dto);
            var handler = new UpdateProjectVersionHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Project_Version_Throws_Project_Version_Not_Found_Exception()
        {
            var projectVersionId = 1;
            var project = new Project()
            {
                Id = 1,
                ProjectVersions = new List<ProjectVersion>()
            };
           
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);

            var dto = new UpdateProjectVersionDto("1.0.1", "test");
            var command = new UpdateProjectVersion(project.Id, projectVersionId, dto);
            var handler = new UpdateProjectVersionHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);
            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectVersionNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
