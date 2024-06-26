﻿using MediatR;

namespace DeployApp.Application.Commands
{
    public record RemoveProject(int project_id) : IRequest;
}
