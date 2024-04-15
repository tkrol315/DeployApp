﻿using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Queries
{
    public record GetInstanceAsDto(int project_id, int instance_id) : IRequest<GetInstanceDto>;
}
