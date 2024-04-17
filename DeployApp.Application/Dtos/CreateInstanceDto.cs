﻿using DeployApp.Domain.Entities;

namespace DeployApp.Application.Dtos
{
    public record CreateInstanceDto(
        string TypeDescription,
        string Name,
        string Key,
        string Secret,
        int? ProjectVersionId
        );
}
