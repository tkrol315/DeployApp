using DeployApp.Application.Abstractions;
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
    public class GetProjectVersionAsDtoTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        private readonly Mock<IProjectVersionConverter> _projectVersionConverter = new();

        [Fact]
        public async Task Handle_Get_Project_Version_As_Dto_Success()
        {
            var projectVersionId = 1;
            var project = new Project() 
            { 
                Id = 1 ,
                ProjectVersions = new List<ProjectVersion>()
                {
                    new()
                    {
                        Id = projectVersionId,
                        Major = 1,
                        Minor = 0,
                        Patch = 0
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);
            _projectVersionConverter.Setup(v => v.VersionToVersionString(1, 0, 0)).Returns("1.0.0");

            var query = new GetProjectVersionAsDto(project.Id,projectVersionId);
            var handler = new GetProjectVersionAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConverter.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeOfType<GetProjectVersionDto>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
           _projectVersionConverter.Verify(x => x.VersionToVersionString(1,0,0), Times.Once);
            
        }
        [Fact]
        public async Task Handle_Get_Project_Version_As_Dto_Throws_Project_Not_Found_Exception()
        {

            var query = new GetProjectVersionAsDto(It.IsAny<int>(), It.IsAny<int>());
            var handler = new GetProjectVersionAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConverter.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverter.Verify(x => x.VersionToVersionString(1, 0, 0), Times.Never);

        }

        [Fact]
        public async Task Handle_Get_Project_Version_As_Dto_Throws_Project_Version_Not_Found_Exception()
        {
            var projectVersionId = 1;
            var project = new Project()
            {
                Id = 1,
                ProjectVersions = new List<ProjectVersion>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithProjectVersionsByIdAsync(project.Id)).ReturnsAsync(project);

            var query = new GetProjectVersionAsDto(project.Id, projectVersionId);
            var handler = new GetProjectVersionAsDtoHandler(_projectRepositoryMock.Object, _projectVersionConverter.Object);

            var act = ()=> handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectVersionNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithProjectVersionsByIdAsync(It.IsAny<int>()), Times.Once);
            _projectVersionConverter.Verify(x => x.VersionToVersionString(1, 0, 0), Times.Never);

        }
    }
}
