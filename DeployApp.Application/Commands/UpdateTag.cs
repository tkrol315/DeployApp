﻿using DeployApp.Application.Dtos;
using MediatR;

namespace DeployApp.Application.Commands
{
    public record UpdateTag(int tag_id, UpdateTagDto updateTagDto) : IRequest;
  


}
