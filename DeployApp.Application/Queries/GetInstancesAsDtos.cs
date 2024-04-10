using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetInstancesAsDtos(int projectId, InstanceSearchPhraseDto searchPhrase) : IRequest<List<GetInstanceDto>>;
}
