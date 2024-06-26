﻿using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record AssignGroup(int project_id, Guid instance_id, AssignGroupDto dto) : IRequest;
}
