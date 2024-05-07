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
using System.Data;

namespace DeployApp.UnitTests.AssignCommandHandlers
{
    public class AssignTagHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IInstanceRepository> _instanceRepositoryMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<ITagRepository> _tagRepositoryMock = new();
        private readonly Mock<ITransactionHandler> _transactionHandlerMock = new();
        private readonly Mock<IDbTransaction> _dbTransactionMock = new();

        public AssignTagHandlerTests()
        {
            _transactionHandlerMock.Setup(x => x.BeginTransaction()).Returns(_dbTransactionMock.Object);
        }

        [Fact]
        public async Task Handle_Assign_Tag_When_Tag_Already_Exists_In_Tag_Collection_Success()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Instances = new List<Instance>() 
                {
                    new() {
                        Id = instanceId,
                        InstanceTags = new List<InstanceTag>() 
                    } 
                }
            };
            var tag = new Tag() {Name = "test" };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _tagRepositoryMock.Setup(t => t.GetTagByNameAsync(It.IsAny<string>())).ReturnsAsync(tag);

            var dto = new AssignTagDto("test", "test");
            var command = new AssignTag(It.IsAny<int>(), instanceId, dto);
            var handler = new AssignTagHandler
                (
                _projectRepositoryMock.Object,
                _tagRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object
                );

            await handler.Handle(command,CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.GetTagByNameAsync(It.IsAny<string>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Once);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateTag>(), CancellationToken.None), Times.Never);
            _transactionHandlerMock.Verify(x => x.BeginTransaction(), Times.Once);
            _dbTransactionMock.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handle_Assign_Tag_When_Tag_Does_Not_Exists_In_Tag_Collection_Success()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Instances = new List<Instance>()
                {
                    new() {
                        Id = instanceId,
                        InstanceTags = new List<InstanceTag>()
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);

            var dto = new AssignTagDto("test", "test");
            var command = new AssignTag(It.IsAny<int>(), instanceId, dto);
            var handler = new AssignTagHandler
                (
                _projectRepositoryMock.Object,
                _tagRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object
                );

            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.GetTagByNameAsync(It.IsAny<string>()), Times.Once);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Once);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateTag>(), CancellationToken.None), Times.Once);
            _transactionHandlerMock.Verify(x => x.BeginTransaction(), Times.Once);
            _dbTransactionMock.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handle_Assign_Tag_Throws_Project_Not_Found_Exception()
        {
          
            var dto = new AssignTagDto("test", "test");
            var command = new AssignTag(It.IsAny<int>(), It.IsAny<Guid>(), dto);
            var handler = new AssignTagHandler
                (
                _projectRepositoryMock.Object,
                _tagRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object
                );

           var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.GetTagByNameAsync(It.IsAny<string>()), times: Times.Never);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateTag>(), CancellationToken.None), Times.Never);
            _transactionHandlerMock.Verify(x => x.BeginTransaction(), Times.Never);
            _dbTransactionMock.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Tag_Throws_Instance_Not_Found_Exception()
        {
            var project = new Project()
            {
                Instances = new List<Instance>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);

            var dto = new AssignTagDto("test", "test");
            var command = new AssignTag(It.IsAny<int>(), It.IsAny<Guid>(), dto);
            var handler = new AssignTagHandler
                (
                _projectRepositoryMock.Object,
                _tagRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object
                );

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.GetTagByNameAsync(It.IsAny<string>()), Times.Never);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateTag>(), CancellationToken.None), Times.Never);
            _transactionHandlerMock.Verify(x => x.BeginTransaction(), Times.Never);
            _dbTransactionMock.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async Task Handle_Assign_Tag_Throws_Tag_With_Name_Already_Assigned_To_Instance_Exception()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Instances = new List<Instance>()
                {
                    new() {
                        Id = instanceId,
                        InstanceTags = new List<InstanceTag>()
                        {
                            new()
                            {
                                Tag = new() {Name = "TEST"}
                            }
                        }
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>())).ReturnsAsync(project);

            var dto = new AssignTagDto("test", "test");
            var command = new AssignTag(It.IsAny<int>(), instanceId, dto);
            var handler = new AssignTagHandler
                (
                _projectRepositoryMock.Object,
                _tagRepositoryMock.Object,
                _instanceRepositoryMock.Object,
                _mediatorMock.Object,
                _transactionHandlerMock.Object
                );

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<TagWithNameAlreadyAssignedToInstanceException>();

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndTagsByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.GetTagByNameAsync(It.IsAny<string>()), Times.Never);
            _instanceRepositoryMock.Verify(x => x.UpdateInstanceAsync(It.IsAny<Instance>()), Times.Never);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateTag>(), CancellationToken.None), Times.Never);
            _transactionHandlerMock.Verify(x => x.BeginTransaction(), Times.Never);
            _dbTransactionMock.Verify(x => x.Commit(), Times.Never);
        }
    }
}
