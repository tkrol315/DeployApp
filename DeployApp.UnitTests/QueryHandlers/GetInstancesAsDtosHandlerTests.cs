using DeployApp.Application.Abstractions;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.UnitTests.TestUtils;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.QueryHandlers
{
    public class GetInstancesAsDtosHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IInstanceRepository> _instanceRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConverteMock = new();

        [Fact]
        public async Task Handle_Get_Instances_As_Dtos_Without_Filter_Success()
        {
            var projectId = 1;
            var instancesAsAsnycQuerable = (new List<Instance>()
            {
                new()
                {
                    Type = new(),
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                    ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                },
                new()
                {
                    Type = new(),
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                    ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 1
                    }
                }
            }).AsAsyncQueryable();
            _projectRepositoryMock.Setup(p => p.ProjectWithIdExistsAsync(projectId)).ReturnsAsync(true);
            _instanceRepositoryMock.Setup(i => i.GetAllAsIQueryable(projectId)).Returns(instancesAsAsnycQuerable);
            _projectVersionConverteMock.Setup(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<string>());

            var query = new GetInstancesAsDtos(projectId, new InstanceFilterDto());
            var handler = new GetInstancesAsDtosHandler(
                _instanceRepositoryMock.Object, _projectVersionConverteMock.Object, _projectRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetInstanceDto>>();
            result.Should().HaveCount(2);
            _projectRepositoryMock.Verify(x => x.ProjectWithIdExistsAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.GetAllAsIQueryable(It.IsAny<int>()), Times.Once);
            _projectVersionConverteMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_Get_Instances_As_Dtos_With_Type_Description_Filter_Success()
        {
            var projectId = 1;
            var filter = "test1";
            var instancesAsAsnycQuerable = (new List<Instance>()
            {
                new()
                {
                    Type = new()
                    {
                        Description = filter
                    },
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                    ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                },
                new()
                {
                    Type = new()
                    {
                        Description = "test2"
                    },
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                     ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                },
                   new()
                {  
                    Type = new()
                    {
                        Description = "test3"
                    },
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                     ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                }
            }).AsAsyncQueryable();
            _projectRepositoryMock.Setup(p => p.ProjectWithIdExistsAsync(projectId)).ReturnsAsync(true);
            _instanceRepositoryMock.Setup(i => i.GetAllAsIQueryable(projectId)).Returns(instancesAsAsnycQuerable);
            _projectVersionConverteMock.Setup(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<string>());

            var query = new GetInstancesAsDtos(projectId, new InstanceFilterDto(filter));
            var handler = new GetInstancesAsDtosHandler(
                _instanceRepositoryMock.Object, _projectVersionConverteMock.Object, _projectRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetInstanceDto>>();
            result.Should().HaveCount(1);
            _projectRepositoryMock.Verify(x => x.ProjectWithIdExistsAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.GetAllAsIQueryable(It.IsAny<int>()), Times.Once);
            _projectVersionConverteMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public async Task Handle_Get_Instances_As_Dtos_With_Version_Filter_Success()
        {
            var projectId = 1;
            var filter = "1.0.0";
            var instancesAsAsnycQuerable = (new List<Instance>()
            {
                new()
                {
                    Type = new()
                    {
                        Description = filter
                    },
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                    ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                },
                new()
                {
                    Type = new()
                    {
                        Description = "test2"
                    },
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                     ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 1
                    }
                },
                   new()
                {
                    Type = new()
                    {
                        Description = "test3"
                    },
                    InstanceTags = new List<InstanceTag>(),
                    InstanceGroups = new List<InstanceGroup>(),
                     ProjectVersion = new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                }
            }).AsAsyncQueryable();
            _projectRepositoryMock.Setup(p => p.ProjectWithIdExistsAsync(projectId)).ReturnsAsync(true);
            _instanceRepositoryMock.Setup(i => i.GetAllAsIQueryable(projectId)).Returns(instancesAsAsnycQuerable);
            _projectVersionConverteMock.Setup(v => v.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(It.IsAny<string>());

            var query = new GetInstancesAsDtos(projectId, new InstanceFilterDto(filter));
            var handler = new GetInstancesAsDtosHandler(
                _instanceRepositoryMock.Object, _projectVersionConverteMock.Object, _projectRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetInstanceDto>>();
            result.Should().HaveCount(1);
            _projectRepositoryMock.Verify(x => x.ProjectWithIdExistsAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.GetAllAsIQueryable(It.IsAny<int>()), Times.Once);
            _projectVersionConverteMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Get_Instances_As_Dtos_Throws_Project_Not_Found_Exception()
        {
            var query = new GetInstancesAsDtos(It.IsAny<int>(), new InstanceFilterDto());
            var handler = new GetInstancesAsDtosHandler(
                _instanceRepositoryMock.Object, _projectVersionConverteMock.Object, _projectRepositoryMock.Object);
            var act = ()=>  handler.Handle(query, CancellationToken.None);

          await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.ProjectWithIdExistsAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.GetAllAsIQueryable(It.IsAny<int>()), Times.Never);
            _projectVersionConverteMock.Verify(x => x.VersionToVersionString(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

       
    }
}
