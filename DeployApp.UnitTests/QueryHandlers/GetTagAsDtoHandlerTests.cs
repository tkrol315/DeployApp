using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.QueryHandlers
{
    public class GetTagAsDtoHandlerTests
    {
        private readonly Mock<ITagRepository> _tagRepositoryMock = new();
        [Fact]
        public async Task Handle_Get_Tag_As_Dto_Success()
        {
            var tag = new Tag() { Id = 1 };
            _tagRepositoryMock.Setup(t => t.GetTagByIdAsync(tag.Id)).ReturnsAsync(tag);

            var query = new GetTagAsDto(tag.Id);
            var handler = new GetTagAsDtoHandler(_tagRepositoryMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetTagDto>();
            _tagRepositoryMock.Verify(x => x.GetTagByIdAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task Handle_Get_Tag_As_Dto_Throws_Tag_Not_Found_Exception()
        {
            var query = new GetTagAsDto(It.IsAny<int>());
            var handler = new GetTagAsDtoHandler(_tagRepositoryMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<TagNotFoundException>();
            _tagRepositoryMock.Verify(x => x.GetTagByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
