using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.RemoveCommandHandlers
{
    public class RemoveInstanceHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();

        [Fact]
        public async Task Handle_Remove_Instance_Success()
        {
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Id = 1,
                Instances = new List<Instance>()
                {
                    new()
                    {
                        Id = instanceId
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveInstance(project.Id, instanceId);
            var handler = new RemoveInstanceHandler(_projectRepositoryMock.Object);
            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }


        [Fact]
        public async Task Handle_Remove_Instance_Throws_Project_Not_Found_Exception()
        {
           
            var command = new RemoveInstance(It.IsAny<int>(), It.IsAny<Guid>());
            var handler = new RemoveInstanceHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Remove_Instance_Throws_Instance_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                Instances = new List<Instance>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveInstance(project.Id, It.IsAny<Guid>());
            var handler = new RemoveInstanceHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceNotFoundException>(); 
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
