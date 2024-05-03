using DeployApp.Application.Abstractions;
using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;

namespace DeployApp.UnitTests.AssignCommandHandlers
{
    public class AssignGroupHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IInstanceRepository> _instanceRepositoryMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();
        private readonly Mock<ITransactionHandler> _transactionHandlerMock = new();

        [Fact]
        public async Task Handle_Assign_Group_To_Instance_When_Group_Already_Exists_Success()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Instances = new List<Instance>()
                {
                    new()
                    {
                        Id = instanceId,
                        InstanceGroups = new List<InstanceGroup>()
                    }
                }
            };
            var group = new Group();
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _groupRepositoryMock.Setup(g => g.GetGroupByNameAsync(It.IsAny<string>())).ReturnsAsync(group);

            var dto = new AssignGroupDto("test");
            var command = new AssignGroup(It.IsAny<int>(), instanceId, dto);
            var handler = new AssignGroupHandler(
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object);

            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GetGroupByNameAsync(It.IsAny<string>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Once);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateGroup>(),CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Group_To_Instance_Creates_Group_And_Assign_To_Instance_When_Group_Does_Not_Exist_Success()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Instances = new List<Instance>()
                {
                    new()
                    {
                        Id = instanceId,
                        InstanceGroups = new List<InstanceGroup>()
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);

            var dto = new AssignGroupDto("test", "test");
            var command = new AssignGroup(It.IsAny<int>(), instanceId, dto);
            var handler = new AssignGroupHandler(
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object);

            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GetGroupByNameAsync(It.IsAny<string>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Once);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateGroup>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_Assign_Group_To_Instance_Throws_Project_Not_Found_Exception()
        {

            var dto = new AssignGroupDto("test");
            var command = new AssignGroup(It.IsAny<int>(), Guid.NewGuid(), dto);
            var handler = new AssignGroupHandler(
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GetGroupByNameAsync(It.IsAny<string>()), Times.Never);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateGroup>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Group_To_Instance_Throws_Instance_Not_Found_Exception()
        {
            var project = new Project()
            {
                Instances = new List<Instance>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);

            var dto = new AssignGroupDto("test");
            var command = new AssignGroup(It.IsAny<int>(), Guid.NewGuid(), dto);
            var handler = new AssignGroupHandler(
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);


            await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GetGroupByNameAsync(It.IsAny<string>()), Times.Never);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateGroup>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Group_To_Instance_Throws_Group_With_Name_Already_Assigned_To_Instance_Exception()
        {
            var instanceId = Guid.NewGuid();
            var groupName = "test";
            var project = new Project()
            {
                Instances = new List<Instance>()
                {
                    new()
                    {
                        Id = instanceId,
                        InstanceGroups = new List<InstanceGroup>()
                        {
                            new()
                            {
                                Group = new(){ Name = groupName }
                            }
                        }
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);

            var dto = new AssignGroupDto(groupName);
            var command = new AssignGroup(It.IsAny<int>(), instanceId, dto);
            var handler = new AssignGroupHandler(
                _projectRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<GroupWithNameAlreadyAssignedToInstanceException>();

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndGroupsByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GetGroupByNameAsync(It.IsAny<string>()), Times.Never);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateGroup>(), CancellationToken.None), Times.Never);
        }
    }
}
