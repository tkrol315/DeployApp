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
    public class GetInstanceAsDtoTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _versionConverterMock = new();

        [Fact]
        public async Task Handle_Get_Instance_As_Dto_Success()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Id = 1,
                Instances = new List<Instance>()
                {
                    new()
                    {
                        Id = instanceId,
                        Type = new(),
                        ProjectVersion = new()
                        {
                            Major = 1,
                            Minor = 0,
                            Patch = 0
                        },
                        InstanceTags = new List<InstanceTag>(),
                        InstanceGroups = new List<InstanceGroup>()
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _versionConverterMock.Setup(v => v.VersionToVersionString(1, 0, 0)).Returns("1.0.0");

            var query = new GetInstanceAsDto(project.Id, instanceId);
            var handler = new GetInstanceAsDtoHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetInstanceDto>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Get_Instance_As_Dto_Throws_Project_Not_Found_Exception()
        {
            var query = new GetInstanceAsDto(It.IsAny<int>(), It.IsAny<Guid>());
            var handler = new GetInstanceAsDtoHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Get_Instance_As_Dto_Throws_Instance_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                Instances = new List<Instance>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);

            var query = new GetInstanceAsDto(project.Id, Guid.NewGuid());
            var handler = new GetInstanceAsDtoHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}
