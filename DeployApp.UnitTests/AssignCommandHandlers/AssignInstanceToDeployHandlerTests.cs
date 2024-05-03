using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.Domain.Enums;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.AssignCommandHandlers
{
    public class AssignInstanceToDeployHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();       

        [Fact]
        public async Task Handle_Assign_Instance_To_Deploy_Returns_Instance_Id()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project() {
                Id = 1,
                Instances = new List<Instance>()
                {
                    new() { Id = instanceId }
                },
                Deploys = new List<Deploy>()
                {
                    new(){
                        Id = 1,
                        DeployInstances = new List<DeployInstance>()
                    },
                }
            };
            _projectRepositoryMock.Setup(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);

            var dto = new AssignInstanceToDeployDto(instanceId, Status.Available);
            var command = new AssignInstanceToDeploy(1, 1,dto);
            var handler = new AssignInstanceToDeployHandler(_projectRepositoryMock.Object);

            var id = await handler.Handle(command,CancellationToken.None);

            id.Should().Be(It.IsAny<Guid>());
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Assign_Instance_To_Deploy_Throws_Project_Not_Found_Exception()
        {
         
            var dto = new AssignInstanceToDeployDto(Guid.NewGuid(), Status.Available);
            var command = new AssignInstanceToDeploy(1, 1, dto);
            var handler = new AssignInstanceToDeployHandler(_projectRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Instance_To_Deploy_Throws_Deploy_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };
            _projectRepositoryMock.Setup(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);

            var dto = new AssignInstanceToDeployDto(Guid.NewGuid(), Status.Available);
            var command = new AssignInstanceToDeploy(1, 1, dto);
            var handler = new AssignInstanceToDeployHandler(_projectRepositoryMock.Object);

           var act = ()=>  handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Instance_To_Deploy_Throws_Instance_Already_Assigned_To_Deploy_Exception()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new(){
                        Id = 1,
                        DeployInstances = new List<DeployInstance>(){ new() { Instance = new() { Id = instanceId} } }
                    },
                }
            };
            _projectRepositoryMock.Setup(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);

            var dto = new AssignInstanceToDeployDto(instanceId, Status.Available);
            var command = new AssignInstanceToDeploy(1, 1, dto);
            var handler = new AssignInstanceToDeployHandler(_projectRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceAlreadyAssignedToDeployException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Instance_To_Deploy_Throws_Instance_Not_Found_Exception()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Id = 1,
                Instances = new List<Instance>(),
                Deploys = new List<Deploy>()
                {
                    new(){
                        Id = 1,
                        DeployInstances = new List<DeployInstance>()
                    },
                }
            };
            _projectRepositoryMock.Setup(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(project);

            var dto = new AssignInstanceToDeployDto(instanceId, Status.Available);
            var command = new AssignInstanceToDeploy(1, 1, dto);
            var handler = new AssignInstanceToDeployHandler(_projectRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

           await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
