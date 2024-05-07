using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.RemoveCommandHandlers
{
    public class RemoveTagHandlerTests
    {
        private readonly Mock<ITagRepository> _tagRepositoryMock = new();

        [Fact]
        public async Task Handle_Remove_Tag_Success()
        {
            var tag = new Tag() { Id = 1 };
            _tagRepositoryMock.Setup(t => t.GetTagByIdAsync(tag.Id)).ReturnsAsync(tag);
            var command = new RemoveTag(tag.Id);
            var handler = new RemoveTagHandler(_tagRepositoryMock.Object);  

            await handler.Handle(command, CancellationToken.None);

            _tagRepositoryMock.Verify(x => x.GetTagByIdAsync(tag.Id), Times.Once);
            _tagRepositoryMock.Verify(x => x.RemoveTagAsync(tag), Times.Once);
            
        }

        [Fact]
        public async Task Handle_Remove_Tag_Throws_Tag_Not_Found_Exception()
        {

            var command = new RemoveTag(It.IsAny<int>());
            var handler = new RemoveTagHandler(_tagRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<TagNotFoundException>();
            _tagRepositoryMock.Verify(x => x.GetTagByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.RemoveTagAsync(It.IsAny<Tag>()), Times.Never);

        }
    }
}
