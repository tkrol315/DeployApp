using DeployApp.Application.Commands;
using DeployApp.Application.Commands.Handlers;
using DeployApp.Application.Exceptions;
using DeployApp.Application.Repositories;
using DeployApp.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeployApp.UnitTests.RemoveCommandHandlers
{
    public class RemoveDeployInstanceHandlerTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
        
        [Fact]
        public async Task Handle_Remove_Deploy_Instance_Success()
        {
            var deployId = 1;
            var instanceId = Guid.NewGuid();
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new()
                    {
                        Id = deployId,
                        DeployInstances = new List<DeployInstance>()
                        {
                            new()
                            {
                                InstanceId = instanceId,
                            }
                        }
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveDeployInstance(project.Id, deployId, instanceId);
            var handler = new RemoveDeployInstanceHandler(_projectRepositoryMock.Object);
            await handler.Handle(command, CancellationToken.None);

            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Remove_Deploy_Instance_Throws_Project_Not_Found_Exception()
        {
            
            var command = new RemoveDeployInstance(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>());
            var handler = new RemoveDeployInstanceHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ProjectNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Remove_Deploy_Instance_Throws_Deploy_Not_Found_Exception()
        {
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveDeployInstance(project.Id, It.IsAny<int>(), It.IsAny<Guid>());
            var handler = new RemoveDeployInstanceHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<DeployNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Remove_Deploy_Instance_Throws_Instance_Not_Found_Exception()
        {
            var deployId = 1;
            var project = new Project()
            {
                Id = 1,
                Deploys = new List<Deploy>()
                {
                    new()
                    {
                        Id = deployId,
                        DeployInstances = new List<DeployInstance>()
                    }
                }
            };
            _projectRepositoryMock.Setup(p => p.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(project.Id)).ReturnsAsync(project);

            var command = new RemoveDeployInstance(project.Id, deployId, It.IsAny<Guid>());
            var handler = new RemoveDeployInstanceHandler(_projectRepositoryMock.Object);
            var act = ()=> handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InstanceNotFoundException>();
            _projectRepositoryMock.Verify(x => x.GetProjectWithInstancesAndDeploysWithInstancesByIdAsync(It.IsAny<int>()), Times.Once);
            _projectRepositoryMock.Verify(x => x.UpdateProjectAsync(It.IsAny<Project>()), Times.Never);
        }
    }
}
