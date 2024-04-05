using DeployApp.Application.Repositories;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTag>
    {
        public CreateTagValidator()
        {
            RuleFor(t => t.createTagDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tag name cannot be empty");
             
            RuleFor(t => t.createTagDto.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tag description cannot be empty");
        }
    }
}
