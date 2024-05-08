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
    public class UpdateDeployInstanceHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IInstanceRepository> _instanceRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _versionConverterMock = new();

        [Fact]
        public async Task Handle_Update_Deploy_Instance_With_Version_When_Version_Exists_Success()
        {
            var deployId = 1;
            var instanceId = Guid.NewGuid();
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
                                    Name = "test",
                                    Type = new()
                                }
                            }
                        }
                    }
                },
                ProjectVersions = new List<ProjectVersion>()
                {
                    new()
                    {
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                }
            };
            var dic = new Dictionary<VersionParts, int>()
            {
                {VersionParts.Major, 1},
                {VersionParts.Minor, 0},
                {VersionParts.Patch, 0}
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "test")).ReturnsAsync(false);
            _versionConverterMock.Setup(v => v.VersionStringToDictionary("1.0.0")).Returns(dic);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.0");
            var command = new UpdateDeployInstance(project.Id,deployId, instanceId, dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );
            await handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);  

        }

        [Fact]
        public async Task Handle_Update_Deploy_Instance_With_Version_When_Version_Does_Not_Exists_Success()
        {
            var deployId = 1;
            var instanceId = Guid.NewGuid();
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
                                    Name = "test",
                                    Type = new()
                                }
                            }
                        }
                    }
                },
                ProjectVersions = new List<ProjectVersion>()
            };
            var dic = new Dictionary<VersionParts, int>()
            {
                {VersionParts.Major, 1},
                {VersionParts.Minor, 0},
                {VersionParts.Patch, 0}
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "test")).ReturnsAsync(false);
            _versionConverterMock.Setup(v => v.VersionStringToDictionary("1.0.0")).Returns(dic);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.0" , "test");
            var command = new UpdateDeployInstance(project.Id, deployId, instanceId, dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );
            await handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);

        }


        [Fact]
        public async Task Handle_Update_Deploy_Instance_Without_Version_Success()
        {
            var deployId = 1;
            var instanceId = Guid.NewGuid();
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
                                    Name = "test",
                                    Type = new()
                                }
                            }
                        }
                    }
                },
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "test")).ReturnsAsync(false);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test");
            var command = new UpdateDeployInstance(project.Id, deployId, instanceId, dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );
            await handler.Handle(command, CancellationToken.None);
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);

        }


        [Fact]
        public async Task Handle_Update_Deploy_Instance_Thrwos_Project_Not_Found_Exception()
        {

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.0");
            var command = new UpdateDeployInstance(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>(), dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Deploy_Instance_Throws_Deploy_Not_Found_Exception()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };

            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.0");
            var command = new UpdateDeployInstance(project.Id, deployId, It.IsAny<Guid>(), dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Update_Deploy_Instance_Throws_Instance_Not_Found_Exception()
        {
            var deployId = 1;
            var instanceId = Guid.NewGuid();
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
                },
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.0");
            var command = new UpdateDeployInstance(project.Id, deployId, instanceId, dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Update_Deploy_Instance_Project_Already_Contains_Instance_With_Name_Exception()
        {
            var deployId = 1;
            var instanceId = Guid.NewGuid();
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
                                    Name = "test",
                                    Type = new()
                                }
                            }
                        }
                    }
                },
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "newName")).ReturnsAsync(true);
           
            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.0");
            var command = new UpdateDeployInstance(project.Id, deployId, instanceId, dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectAlreadyContainsInstanceWithNameException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Update_Deploy_Instance_Throws_Version_Description_Missing_Exception()
        {
            var deployId = 1;
            var instanceId = Guid.NewGuid();
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
                                    Name = "test",
                                    Type = new()
                                }
                            }
                        }
                    }
                },
                ProjectVersions = new List<ProjectVersion>()
            };
            var dic = new Dictionary<VersionParts, int>()
            {
                {VersionParts.Major, 1},
                {VersionParts.Minor, 0},
                {VersionParts.Patch, 0}
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithDeployAndInstancesAsync(project.Id)).ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(project.Id, "newName")).ReturnsAsync(false);
            _versionConverterMock.Setup(v => v.VersionStringToDictionary("1.0.0")).Returns(dic);

            var dto = new UpdateInstanceDto("test", "newName", "test", "test", "1.0.0");
            var command = new UpdateDeployInstance(project.Id, deployId, instanceId, dto);
            var handler = new UpdateDeployInstanceHandler
                (
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _versionConverterMock.Object
                );

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<VersionDescriptionMissingException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithDeployAndInstancesAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);

        }
    }
}
