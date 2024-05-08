using DeployApp.Application.Abstractions;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.QueryHandlers
{
    public class GetDeployInstanceAsDtoTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConvertere = new();

        [Fact]
        public async Task Handle_Get_Deploy_Instance_As_Dto_Success()
        {
            var instanceId = Guid.NewGuid();
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new()
                    {
                        Id = deployId,
                        DeployInstances = new List<DeployInstance>()
                        {
                            new()
                            {
                                InstanceId = instanceId,
                                Instance = new()
                                {
                                    Type = new(),
                                    ProjectVersion = new()
                                    {
                                        Major = 1,
                                        Minor = 0,
                                        Patch =0
                                    },
                                    InstanceGroups = new List<InstanceGroup>(),
                                    InstanceTags = new List<InstanceTag>()
                                }
                            }
                        }
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _projectVersionConvertere.Setup(v => v.VersionToVersionString(1, 0, 0)).Returns("1.0.0");

            var query = new GetDeployInstanceAsDto(project.Id, deployId, instanceId);
            var handler = new GetDeployInstanceAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConvertere.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConvertere.Verify(x => x.VersionToVersionString(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task Handle_Get_Deploy_Instance_As_Dto_Throws_Project_Not_Found_Exception()
        {
            var query = new GetDeployInstanceAsDto(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>());
            var handler = new GetDeployInstanceAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConvertere.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConvertere.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Get_Deploy_Instance_As_Dto_Throws_Deploy_Not_Found_Exception()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);

            var query = new GetDeployInstanceAsDto(project.Id, deployId, It.IsAny<Guid>());
            var handler = new GetDeployInstanceAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConvertere.Object);

            var act = () => handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConvertere.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Get_Deploy_Instance_As_Dto_Throws_Instance_Not_Found_Exception()
        {
            var instanceId = Guid.NewGuid();
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new()
                    {
                        Id = deployId,
                        DeployInstances = new List<DeployInstance>()
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);

            var query = new GetDeployInstanceAsDto(project.Id, deployId, instanceId);
            var handler = new GetDeployInstanceAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConvertere.Object);

            var act = () => handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConvertere.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        }
    }
}
