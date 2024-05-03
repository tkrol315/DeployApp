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

namespace DeployApp.UnitTests.CreateCommandHandlers
{
    public class CreateInstanceHandlerTests
    {
        private readonly Mock<IInstanceRepository> _instanceRepositoryMock = new();
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _versionConverterMock = new();

        [Fact]
        public async Task Handle_Create_Instance_Without_Version_Returns_New_Instance_Id()
        {
            var project = new Project() { Instances = new List<Instance>() };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            _instanceRepositoryMock.Setup(i => i.CreateInstanceAsync(It.IsAny<Instance>()))
                .ReturnsAsync(Guid.NewGuid());

            var dto = new CreateInstanceDto("test", "test", "test", "test");
            var command = new CreateInstance(It.IsAny<int>(), dto);
            var handler = new CreateInstanceHandler(
                _instanceRepositoryMock.Object,_projectRepositoryMock.Object,_versionConverterMock.Object
                );

            var guid = await handler.Handle(command, CancellationToken.None);

            guid.Should().NotBeEmpty();
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.CreateInstanceAsync(It.IsAny<Instance>()),Times.Once);

        }

        [Fact]
        public async Task Handle_Create_Instance_With_Existing_Version_Returns_New_Instance_Id()
        {
            var project = new Project() {
                Instances = new List<Instance>(),
                ProjectVersions = new List<ProjectVersion>() {
                    new() {
                        Major =  1,
                        Minor = 0,
                        Patch = 0
                    } 
                } 
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            _instanceRepositoryMock.Setup(i => i.CreateInstanceAsync(It.IsAny<Instance>()))
                .ReturnsAsync(Guid.NewGuid());
            _versionConverterMock.Setup(v => v.VersionStringToDictionary(It.IsAny<string>()))
                .Returns(new Dictionary<VersionParts, int>() {
                    {VersionParts.Major,1 },
                    {VersionParts.Minor, 0 },
                     {VersionParts.Patch, 0 },
                });

            var dto = new CreateInstanceDto("test", "test", "test", "test","1.0.0");
            var command = new CreateInstance(It.IsAny<int>(), dto);
            var handler = new CreateInstanceHandler(
                _instanceRepositoryMock.Object, _projectRepositoryMock.Object, _versionConverterMock.Object
                );

            var guid = await handler.Handle(command, CancellationToken.None);

            guid.Should().NotBeEmpty();
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.CreateInstanceAsync(It.IsAny<Instance>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task Handle_Create_Instance_With_Creating_Version_Returns_New_Instance_Id()
        {
            var project = new Project(){Instances = new List<Instance>(), ProjectVersions = new List<ProjectVersion>()};
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            _instanceRepositoryMock.Setup(i => i.CreateInstanceAsync(It.IsAny<Instance>()))
                .ReturnsAsync(Guid.NewGuid());
            _versionConverterMock.Setup(v => v.VersionStringToDictionary(It.IsAny<string>()))
                .Returns(new Dictionary<VersionParts, int>() {
                    {VersionParts.Major,1 },
                    {VersionParts.Minor, 0 },
                     {VersionParts.Patch, 0 },
                });

            var dto = new CreateInstanceDto("test", "test", "test", "test", "1.0.0","test");
            var command = new CreateInstance(It.IsAny<int>(), dto);
            var handler = new CreateInstanceHandler(
                _instanceRepositoryMock.Object, _projectRepositoryMock.Object, _versionConverterMock.Object
                );

            var guid = await handler.Handle(command, CancellationToken.None);

            guid.Should().NotBeEmpty();
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.CreateInstanceAsync(It.IsAny<Instance>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task Handle_Create_Instance_Throws_Project_Not_Found_Exception()
        {

            var dto = new CreateInstanceDto("test", "test", "test", "test");
            var command = new CreateInstance(It.IsAny<int>(), dto);
            var handler = new CreateInstanceHandler(
                _instanceRepositoryMock.Object, _projectRepositoryMock.Object, _versionConverterMock.Object
                );

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _instanceRepositoryMock.Verify(x => x.CreateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Create_Instance_Throws_Project_Already_Contains_Instance_With_Name_Exception()
        {
            var project = new Project() { Instances = new List<Instance>() };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()))
               .ReturnsAsync(true);

            var dto = new CreateInstanceDto("test", "test", "test", "test");
            var command = new CreateInstance(It.IsAny<int>(), dto);
            var handler = new CreateInstanceHandler(
                _instanceRepositoryMock.Object, _projectRepositoryMock.Object, _versionConverterMock.Object
                );

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectAlreadyContainsInstanceWithNameException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.CreateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Never);

        }

        [Fact]
        public async Task Handle_Create_Instance_Throws_Version_Description_Missing_Exception()
        {
            var project = new Project() { Instances = new List<Instance>(), ProjectVersions = new List<ProjectVersion>() };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);
            _instanceRepositoryMock.Setup(i => i.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            _versionConverterMock.Setup(v => v.VersionStringToDictionary(It.IsAny<string>()))
                .Returns(new Dictionary<VersionParts, int>() {
                    {VersionParts.Major,1 },
                    {VersionParts.Minor, 0 },
                     {VersionParts.Patch, 0 },
                });

            var dto = new CreateInstanceDto("test", "test", "test", "test", "1.0.0");
            var command = new CreateInstance(It.IsAny<int>(), dto);
            var handler = new CreateInstanceHandler(
                _instanceRepositoryMock.Object, _projectRepositoryMock.Object, _versionConverterMock.Object
                );

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<VersionDescriptionMissingException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.InstanceWithNameAlreadyExists(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            _versionConverterMock.Verify(x => x.VersionStringToDictionary(It.IsAny<string>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.CreateInstanceAsync(It.IsAny<Instance>()), Times.Never);
        }
    }
}
