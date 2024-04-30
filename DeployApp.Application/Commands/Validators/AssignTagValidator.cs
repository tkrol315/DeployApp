using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class AssignTagValidator : AbstractValidator<AssignTagDto>
    {
        public AssignTagValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Tag name cannot be empty")
                .MaximumLength(100)
                .WithMessage("Tag name cannot be longer then 100 characters");
            RuleFor(a => a.Description)
                .MaximumLength(250)
                .WithMessage("Tag description cannot be longer then 250 characters");
        }
    }
}
