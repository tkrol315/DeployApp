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
    public class GetDeployAsDtoTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConverterMock = new();

        [Fact]
        public async Task Handle_Get_Deploy_As_Dto_Success()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new()
                    {
                        Id = deployId,
                        ProjectVersion = new()
                        {
                            Major = 1,
                            Minor=  0,
                            Patch = 0
                        }
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);
            _projectVersionConverterMock.Setup(v => v.VersionToVersionString(1, 0, 0)).Returns("1.0.0");

            var query = new GetDeployAsDto(project.Id, deployId);
            var handler = new GetDeployAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetDeployDto>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionToVersionString(1, 0, 0), Times.Once);
        }

        [Fact]
        public async Task Handle_Get_Deploy_As_Dto_Throws_Project_Not_Found_Exception()
        {
            var query = new GetDeployAsDto(It.IsAny<int>(), It.IsAny<int>());
            var handler = new GetDeployAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionToVersionString(1, 0, 0), Times.Never);
        }

        [Fact]
        public async Task Handle_Get_Deploy_As_Dto_Throws_Deploy_Not_Found_Exception()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };

            _projectRepositoryMock.Setup(p => p.GetProjectWithDeploysByIdAsync(project.Id)).ReturnsAsync(project);

            var query = new GetDeployAsDto(project.Id, deployId);
            var handler = new GetDeployAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConverterMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeploysByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverterMock.Verify(x => x.VersionToVersionString(1, 0, 0), Times.Never);
        }
    }
}
