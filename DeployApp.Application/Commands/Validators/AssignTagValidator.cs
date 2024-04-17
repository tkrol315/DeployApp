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
                .WithMessage("TagName cannot be empty");
        }
    }
}
