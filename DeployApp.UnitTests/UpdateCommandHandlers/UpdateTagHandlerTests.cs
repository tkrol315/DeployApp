using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.UpdateCommandHandlers
{
    public class UpdateTagHandlerTests
    {
        private readonly Mock<ITagRepository> _tagRepositoryMock = new();
        [Fact]
        public async Task Handle_Update_Tag_Success()
        {

            var tag = new Tag() { Id = 1, Name = "test"};
            _tagRepositoryMock.Setup(t => t.GetTagByIdAsync(tag.Id)).ReturnsAsync(tag);
            _tagRepositoryMock.Setup(t => t.TagWithNameAlreadyExistsAsync(tag.Name)).ReturnsAsync(false);

            var dto = new UpdateTagDto("newName", "newDescription");
            var command = new UpdateTag(tag.Id, dto);
            var handler = new UpdateTagHandler(_tagRepositoryMock.Object);

            await handler.Handle(command, CancellationToken.None);

            _tagRepositoryMock.Verify(x => x.GetTagByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.TagWithNameAlreadyExistsAsync(It.IsAny<string>()), Times.Once);    
            _tagRepositoryMock.Verify(x => x.UpdateTagAsync(It.IsAny<Tag>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Update_Tag_Throws_Tag_Not_Found_Exception() { 

            var dto = new UpdateTagDto("newName", "newDescription");
            var command = new UpdateTag(It.IsAny<int>(), dto);
            var handler = new UpdateTagHandler(_tagRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<TagNotFoundException>();
            _tagRepositoryMock.Verify(x => x.GetTagByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.TagWithNameAlreadyExistsAsync(It.IsAny<string>()), Times.Never);
            _tagRepositoryMock.Verify(x => x.UpdateTagAsync(It.IsAny<Tag>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Update_Tag_Throws_Tag_With_Name_Already_Exists_Exception()
        {

            var tag = new Tag() { Id = 1, Name = "test" };
            _tagRepositoryMock.Setup(t => t.GetTagByIdAsync(tag.Id)).ReturnsAsync(tag);
            _tagRepositoryMock.Setup(t => t.TagWithNameAlreadyExistsAsync("TEST")).ReturnsAsync(true);

            var dto = new UpdateTagDto("TEST", "newDescription");
            var command = new UpdateTag(tag.Id, dto);
            var handler = new UpdateTagHandler(_tagRepositoryMock.Object);

            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<TagWithNameAlreadyExistsException>();
            _tagRepositoryMock.Verify(x => x.GetTagByIdAsync(It.IsAny<int>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.TagWithNameAlreadyExistsAsync(It.IsAny<string>()), Times.Once);
            _tagRepositoryMock.Verify(x => x.UpdateTagAsync(It.IsAny<Tag>()), Times.Never);
        }
    }
}
