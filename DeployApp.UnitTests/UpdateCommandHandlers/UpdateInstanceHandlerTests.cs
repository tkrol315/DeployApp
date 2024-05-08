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
    public class UpdateInstanceHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IInstanceRepository> _instanceRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _versionConverterMock = new();

        [Fact]
        public async Task Handle_Update_Instance_When_Version_String_Not_Empty_And_Version_Exists_Success()
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
                        Name = "test",
                        Type = new(),
                        ProjectVersion = new()
                        {
                            Major = 1,
                            Minor = 0,
                            Patch = 0
                        }
                    }
                },
                ProjectVersions = new List<ProjectVersion>() 
                {
                    new(){Major = 1, Minor = 0, Patch = 0},
                    new(){Major = 1, Minor = 0, Patch = 1}
                }
            };
            var dic = new Dictionary<VersionParts, int>()
            {
                {VersionParts.Major, 1},
                {VersionParts.Minor, 0},
                {VersionParts.Patch, 1},
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "newName")).ReturnsAsync(false);
            _versionConverterMock.Setup(pv => pv.VersionStringToDictionary("1.0.1")).Returns(dic);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.1", "test");
            var command = new UpdateInstance(project.Id,instanceId,dto);
            var handler = new UpdateInstanceHandler
                (
                    _projectRepositoryMock.Object,
                    _instanceRepositoryMock.Object,
                    _versionConverterMock.Object
                );

            await handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Instance_When_Version_String_Not_Empty_And_Version_Does_Not_Exists_Success()
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
                        Name = "test",
                        Type = new(),
                        ProjectVersion = new()
                        {
                            Major = 1,
                            Minor = 0,
                            Patch = 0
                        }
                    }
                },
                ProjectVersions = new List<ProjectVersion>()
                {
                    new(){Major = 1, Minor = 0, Patch = 0},
                }
            };
            var dic = new Dictionary<VersionParts, int>()
            {
                {VersionParts.Major, 1},
                {VersionParts.Minor, 0},
                {VersionParts.Patch, 1},
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "newName")).ReturnsAsync(false);
            _versionConverterMock.Setup(pv => pv.VersionStringToDictionary("1.0.1")).Returns(dic);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.1", "test");
            var command = new UpdateInstance(project.Id, instanceId, dto);
            var handler = new UpdateInstanceHandler
                (
                    _projectRepositoryMock.Object,
                    _instanceRepositoryMock.Object,
                    _versionConverterMock.Object
                );

            await handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Instance_When_Version_String_Empty_Success()
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
                        Name = "test",
                        Type = new(),
                    }
                },
                ProjectVersions = new List<ProjectVersion>()
            };

            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "newName")).ReturnsAsync(false);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test");
            var command = new UpdateInstance(project.Id, instanceId, dto);
            var handler = new UpdateInstanceHandler
                (
                    _projectRepositoryMock.Object,
                    _instanceRepositoryMock.Object,
                    _versionConverterMock.Object
                );

            await handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Instance_Throws_Project_Not_Found_Exception()
        {

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.1", "test");
            var command = new UpdateInstance(It.IsAny<int>(), It.IsAny<Guid>(), dto);
            var handler = new UpdateInstanceHandler
                (
                    _projectRepositoryMock.Object,
                    _instanceRepositoryMock.Object,
                    _versionConverterMock.Object
                );

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Instance_Throws_Instance_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                Instances = new List<Instance>(),
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.1", "test");
            var command = new UpdateInstance(project.Id, It.IsAny<Guid>(), dto);
            var handler = new UpdateInstanceHandler
                (
                    _projectRepositoryMock.Object,
                    _instanceRepositoryMock.Object,
                    _versionConverterMock.Object
                );

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Instance_Throws_Project_Already_Contains_Instance_With_Name_Exception()
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
                        Name = "test",
                        Type = new(),
                    }
                },
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "newName")).ReturnsAsync(true);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.1", "test");
            var command = new UpdateInstance(project.Id, instanceId, dto);
            var handler = new UpdateInstanceHandler
                (
                    _projectRepositoryMock.Object,
                    _instanceRepositoryMock.Object,
                    _versionConverterMock.Object
                );

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectAlreadyContainsInstanceWithNameException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Instance_Throws_Version_Description_Missing_When_Try_To_Update_With_New_Version_Exception()
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
                        Name = "test",
                        Type = new(),
                        ProjectVersion = new()
                        {
                            Major = 1,
                            Minor = 0,
                            Patch = 0
                        }
                    }
                },
                ProjectVersions = new List<ProjectVersion>()
            };
            var dic = new Dictionary<VersionParts, int>()
            {
                {VersionParts.Major, 1},
                {VersionParts.Minor, 0},
                {VersionParts.Patch, 1},
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "newName")).ReturnsAsync(false);
            _versionConverterMock.Setup(pv => pv.VersionStringToDictionary("1.0.1")).Returns(dic);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.1");
            var command = new UpdateInstance(project.Id, instanceId, dto);
            var handler = new UpdateInstanceHandler
                (
                    _projectRepositoryMock.Object,
                    _instanceRepositoryMock.Object,
                    _versionConverterMock.Object
                );

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<VersionDescriptionMissingException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
