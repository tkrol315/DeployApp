using DeployApp.Application.Dtos;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Queries;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DeployApp.UnitTests.TestUtils;

namespace DeployApp.UnitTests.QueryHandlers
{
    public class GetTagsAsDtosHandlerTests
    {
        private readonly Mock<ITagRepository> _tagRepositoryMock = new();

        [Fact]
        public async Task Handle_Get_Tags_As_Dtos_Success()
        {
            var tags = (new List<Tag>() { new(), new() }).AsAsyncQueryable();
            
            _tagRepositoryMock.Setup(t => t.GetTagsAsIQueryable()).Returns(tags);

            var query = new GetTagsAsDtos();
            var handler = new GetTagsAsDtosHandler(_tagRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetTagDto>>();
            result.Should().HaveCount(2);
            _tagRepositoryMock.Verify(x => x.GetTagsAsIQueryable(), Times.Once);
        }

    }
}
