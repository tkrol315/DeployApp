using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Primitives;
using Moq;

namespace DeployApp.UnitTests.UpdateCommandHandlers
{
    public class UpdateGroupHandlerTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();
        [Fact]
        public async Task Handle_Update_Group_Success()
        {
            var group = new Group() { Id = 1, Name = "test" };
            _groupRepositoryMock.Setup(g => g.GetGroupByIdAsync(group.Id)).ReturnsAsync(group);
            _groupRepositoryMock.Setup(g => g.GroupWithNameAlreadyExistsAsync(group.Name)).ReturnsAsync(false);

            var dto = new UpdateGroupDto("newName", "test");
            var command = new UpdateGroup(group.Id, dto);
            var handler = new UpdateGroupHandler(_groupRepositoryMock.Object);

            await handler.Handle(command, CancellationToken.None);
            _groupRepositoryMock.Verify(x => x.GetGroupByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GroupWithNameAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.UpdateGroupAsync(It.IsAny<Group>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Group_Throws_Group_Not_Found_Exception()
        {
            var dto = new UpdateGroupDto("newName", "test");
            var command = new UpdateGroup(It.IsAny<int>(), dto);
            var handler = new UpdateGroupHandler(_groupRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<GroupNotFoundException>();
            _groupRepositoryMock.Verify(x => x.GetGroupByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GroupWithNameAlreadyExistsAsync(It.IsAny<string>()), Times.Never);
            _groupRepositoryMock.Verify(x => x.UpdateGroupAsync(It.IsAny<Group>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Group_Throws_Group_With_Name_Already_Exists_Exception()
        {
            var group = new Group() { Id = 1, Name = "test" };
            _groupRepositoryMock.Setup(g => g.GetGroupByIdAsync(group.Id)).ReturnsAsync(group);
            _groupRepositoryMock.Setup(g => g.GroupWithNameAlreadyExistsAsync("newName")).ReturnsAsync(true);

            var dto = new UpdateGroupDto("newName", "test");
            var command = new UpdateGroup(group.Id, dto);
            var handler = new UpdateGroupHandler(_groupRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<GroupWithNameAlreadyExistsException>();
            _groupRepositoryMock.Verify(x => x.GetGroupByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.GroupWithNameAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _groupRepositoryMock.Verify(x => x.UpdateGroupAsync(It.IsAny<Group>()), Times.Never);
        }
    }
}
