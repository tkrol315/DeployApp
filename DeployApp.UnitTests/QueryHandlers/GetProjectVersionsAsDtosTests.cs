using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.QueryHandlers
{
    public class GetProjectVersionsAsDtosTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConverter = new();
        [Fact]
        public async Task Handle_Get_Project_Versions_As_Dtos_Success()
        {
            var project = new Project()
            {
                Id = 1,
                ProjectVersions = new List<ProjectVersion>() { new() { }, new() }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _projectVersionConverter.Setup(v => v.VersionToVersionString(It.IsAny<int>(),It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<string>());

            var query = new GetProjectVersionsAsDtos(project.Id);
            var handler = new GetProjectVersionsAsDtosHandler(_projectRepositoryMock.Object, _projectVersionConverter.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetProjectVersionDto>>();
            result.Should().HaveCount(2);
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverter.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_Get_Project_Versions_As_Dtos_Throws_Project_Not_Found_Exception()
        {

            var query = new GetProjectVersionsAsDtos(It.IsAny<int>());
            var handler = new GetProjectVersionsAsDtosHandler(_projectRepositoryMock.Object, _projectVersionConverter.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverter.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }


    }
}
