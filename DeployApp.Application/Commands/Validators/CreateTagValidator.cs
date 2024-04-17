﻿using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTag>
    {
        public CreateTagValidator()
        {
            RuleFor(t => t.createTagDto.Name)
                .NotEmpty()
                .WithMessage("Tag name cannot be empty");
        }
    }
}
