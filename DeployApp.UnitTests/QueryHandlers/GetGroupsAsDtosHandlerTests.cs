using DeployApp.Application.Dtos;
using DeployApp.Application.Queries;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using DeployApp.UnitTests.TestUtils;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.QueryHandlers
{
    public class GetGroupsAsDtosHandlerTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();
        [Fact]
        public async Task Handle_Get_Groups_As_Dtos_Success()
        {
            var groups = (new List<Group>() { new(), new() }).AsAsyncQueryable();
            _groupRepositoryMock.Setup(g => g.GetGroupsAsIQueryable()).Returns(groups);

            var query = new GetGroupsAsDtos();
            var handler = new GetGroupsAsDtosHandler(_groupRepositoryMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetGroupDto>>();
            result.Should().HaveCount(2);
            _groupRepositoryMock.Verify(g => g.GetGroupsAsIQueryable(), Times.Once());
        }
    }
}
