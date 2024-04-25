﻿using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateDeployInstance(int project_id, int deploy_id, int instance_id, UpdateInstanceDto dto) : IRequest;
}
