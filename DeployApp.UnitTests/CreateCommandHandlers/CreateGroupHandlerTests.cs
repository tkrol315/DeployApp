using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.CreateCommandHandlers
{
    public class CreateGroupHandlerTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();

        [Fact]
        public async Task Handle_Create_Group_Returns_New_Group_Id()
        {
            _groupRepositoryMock.Setup(g => g.GroupWithNameAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _groupRepositoryMock.Setup(g => g.CreateGroupAsync(It.IsAny<Group>())).ReturnsAsync(1);

            var handler = new CreateGroupHandler(_groupRepositoryMock.Object);
            var group = new CreateGroupDto("test","test");
            var command = new CreateGroup(group);

            var id = await handler.Handle(command, CancellationToken.None);

            id.Should().Be(1);
            _groupRepositoryMock.Verify(x => x.GroupWithNameAlreadyExistsAsync(It.IsAny<string>()),Times.Once);
            _groupRepositoryMock.Verify(x => x.CreateGroupAsync(It.IsAny<Group>()),Times.Once);
        }

        [Fact]
        public async Task Handle_Create_Group_Throws_Group_With_Name_Already_Exists_Exception()
        {
            _groupRepositoryMock.Setup(g => g.GroupWithNameAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new CreateGroupHandler(_groupRepositoryMock.Object);
            var group = new CreateGroupDto("test", "test");
            var command = new CreateGroup(group);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<GroupWithNameAlreadyExistsException>();
            _groupRepositoryMock.Verify(x => x.GroupWithNameAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.CreateGroupAsync(It.IsAny<Group>()), Times.Never);

        }
    }
}
