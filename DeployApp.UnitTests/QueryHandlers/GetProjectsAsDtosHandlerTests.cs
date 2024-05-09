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
    public class GetProjectsAsDtosHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        [Fact]
        public async Task Handle_Get_Projects_As_Dtos_Success()
        {
            var projectsAsAsyncQuerable = (new List<Project> { new(), new() }).AsAsyncQueryable();
            _projectRepositoryMock.Setup(p => p.GetProjectsAsIQueryable()).Returns(projectsAsAsyncQuerable);

            var query = new GetProjectsAsDtos();
            var handler = new GetProjectsAsDtosHandler(_projectRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<List<GetProjectDto>>();
            result.Should().HaveCount(2);
            _projectRepositoryMock.Verify(x => x.GetProjectsAsIQueryable(),Times.Once());
        }
    }
}
