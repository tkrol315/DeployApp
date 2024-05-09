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
    public class GetDeploysAsDtosHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _versionConverterMock = new();

        [Fact]
        public async Task Handle_Get_Deploys_As_Dtos_Success()
        {
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>
            {
                    new()
                    {
                        ProjectVersion = new()
                    },
                    new()
                    {
                        ProjectVersion = new()
                    }
            }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);
            _versionConverterMock.Setup(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<string>());
            var query = new GetDeploysAsDtos(project.Id);
            var handler = new GetDeploysAsDtosHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetDeployDto>>();
            result.Should().HaveCount(2);
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_Get_Deploys_As_Dtos_Throws_Project_Not_Found_Exception()
        {
            var query = new GetDeploysAsDtos(It.IsAny<int>());
            var handler = new GetDeploysAsDtosHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);
            var act = ()=>  handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
    
}
