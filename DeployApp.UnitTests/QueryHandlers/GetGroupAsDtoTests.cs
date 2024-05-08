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
    public class GetGroupAsDtoTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();
        [Fact]
        public async Task Handle_Get_Group_As_Dto_Success()
        {
            var group = new Group() { Id = 1 };
            _groupRepositoryMock.Setup(g => g.GetGroupByIdAsync(group.Id)).ReturnsAsync(group);

            var query = new GetGroupAsDto(group.Id);
            var handler = new GetGroupAsDtoHandler(_groupRepositoryMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetGroupDto>();
            _groupRepositoryMock.Verify(x => x.GetGroupByIdAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task Handle_Get_Group_As_Dto_Throws_Group_Not_Found_Exception()
        {
            var query = new GetGroupAsDto(It.IsAny<int>());
            var handler = new GetGroupAsDtoHandler(_groupRepositoryMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<GroupNotFoundException>();
            _groupRepositoryMock.Verify(x => x.GetGroupByIdAsync(It.IsAny<int>()), Times.Once());
        }
    }
}
