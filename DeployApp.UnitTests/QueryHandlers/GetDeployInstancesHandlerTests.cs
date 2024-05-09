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
    public class GetDeployInstancesHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _versionConverterMock = new();

        [Fact]
        public async Task Handle_Get_Deploy_Instances_As_Dtos_Success()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new(){
                        Id = deployId,

                        DeployInstances = new List<DeployInstance>()
                        {
                            new()
                            {
                                Instance = new()
                                {
                                    ProjectVersion = new(){Major = 1, Minor = 0, Patch = 0},
                                    Type = new(),
                                    InstanceGroups = new List<InstanceGroup>(),
                                    InstanceTags = new List<InstanceTag>()
                                }
                            },
                            new()
                            {
                                Instance = new()
                                {
                                    ProjectVersion = new(){Major = 1, Minor = 0, Patch =1},
                                    Type = new(),
                                    InstanceGroups = new List<InstanceGroup>(),
                                    InstanceTags = new List<InstanceTag>()
                                }
                            }
                        }
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _versionConverterMock.Setup(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<string>());
            var query = new GetDeployInstancesAsDtos(project.Id, deployId, Domain.Enums.Status.Available);
            var handler = new GetDeployInstancesAsDtosHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetInstanceDto>>();
            result.Should().HaveCount(2);
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));

        }

        [Fact]
        public async Task Handle_Get_Deploy_Instances_As_Dtos_Throws_Project_Not_Found_Exception()
        {
            var query = new GetDeployInstancesAsDtos(It.IsAny<int>(), It.IsAny<int>(), Domain.Enums.Status.Available);
            var handler = new GetDeployInstancesAsDtosHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);
            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Get_Deploy_Instances_As_Dtos_Throws_Deploy_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _versionConverterMock.Setup(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<string>());
            var query = new GetDeployInstancesAsDtos(project.Id, It.IsAny<int>(), Domain.Enums.Status.Available);
            var handler = new GetDeployInstancesAsDtosHandler(_projectRepositoryMock.Object, _versionConverterMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);

        }
    }
}
