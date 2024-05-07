using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Text.RegularExpressions;


namespace DeployApp.UnitTests.RemoveCommandHandlers
{
    public class RemoveGroupHandlerTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();

        [Fact]
        public async Task Handle_Remove_Group_Success()
        {
            var group = new Domain.Entities.Group() { Id =  1 };
            _groupRepositoryMock.Setup(g => g.GetGroupByIdAsync(It.IsAny<int>())).ReturnsAsync(group);

            var command = new RemoveGroup(group.Id);
            var handler = new RemoveGroupHandler(_groupRepositoryMock.Object);

            await handler.Handle(command, CancellationToken.None);

            _groupRepositoryMock.Verify(g => g.GetGroupByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(g => g.RemoveGroupAsync(group), Times.Once);
        }

        [Fact]
        public async Task Handle_Remove_Group_Throws_Group_Not_Found_Exception()
        {
            var command = new RemoveGroup(It.IsAny<int>());
            var handler = new RemoveGroupHandler(_groupRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<GroupNotFoundException>();

            _groupRepositoryMock.Verify(g => g.GetGroupByIdAsync(It.IsAny<int>()), Times.Once);
            _groupRepositoryMock.Verify(g => g.RemoveGroupAsync(It.IsAny<Domain.Entities.Group>()), Times.Never);
        }
    }
}
