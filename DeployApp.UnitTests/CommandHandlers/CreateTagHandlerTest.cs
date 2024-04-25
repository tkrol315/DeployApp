using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.CommandHandlers
{
    public class CreateTagHandlerTest
    {
        private readonly Mock<ITagRepository> _tagRepoMock = new();

        [Fact]
        public async Task Handle_Create_Tag_Returns_New_Tag_Id()
        {
            _tagRepoMock.Setup(t => t.TagWithNameAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _tagRepoMock.Setup(t => t.AddTagAsync(It.IsAny<Tag>())).ReturnsAsync(1);

            var createTagHandler = new CreateTagHandler(_tagRepoMock.Object);
            var tagDto = new CreateTagDto("tagName", "tagDescription");
            var createTagCommand = new CreateTag(tagDto);

            var newTagId = await createTagHandler.Handle(createTagCommand, CancellationToken.None);

            newTagId.Should().Be(1);
        }

        [Fact]
        public async Task Handle_Create_Task_Throws_Tag_With_Name_Already_Exists_Exception()
        {
            _tagRepoMock.Setup(t => t.TagWithNameAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var createTagHandler = new CreateTagHandler(_tagRepoMock.Object);
            var tagDto = new CreateTagDto("tagName", "tagDescription");
            var createTagCommand = new CreateTag(tagDto);

            var act = ()=> createTagHandler.Handle(createTagCommand, CancellationToken.None);

            await act.Should().ThrowAsync<TagWithNameAlreadyExistsException>();
        }
    }
}
