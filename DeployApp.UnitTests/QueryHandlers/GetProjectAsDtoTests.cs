using DeployApp.Application.Dtos;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Queries;
using DeployApp.Application.Queries.Handlers;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;

namespace DeployApp.UnitTests.QueryHandlers
{
    public class GetProjectAsDtoTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        [Fact]
        public async Task Handle_Get_Project_As_Dto_Success()
        {
            var project = new Project() { Id = 1, };
            _projectRepositoryMock.Setup(p => p.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);

            var query = new GetProjectAsDto(project.Id);
            var handler = new GetProjectAsDtoHandler(_projectRepositoryMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetProjectDto>();
            _projectRepositoryMock.Verify(x => x.GetProjectByIdAsync(It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public async Task Handle_Get_Project_As_Dto_Throws_Project_Not_Found_Exception()
        {
            var query = new GetProjectAsDto(It.IsAny<int>());
            var handler = new GetProjectAsDtoHandler(_projectRepositoryMock.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
