﻿using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupDto>
    {
        public UpdateGroupValidator()
        {
            RuleFor(g => g.Name)
               .NotEmpty()
               .WithMessage("Group name cannot be empty");
        }
    }
}
