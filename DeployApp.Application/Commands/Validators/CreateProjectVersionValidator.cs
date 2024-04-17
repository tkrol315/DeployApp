using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateProjectVersionValidator : AbstractValidator<CreateProjectVersionDto>
    {
        public CreateProjectVersionValidator()
        {
            RuleFor(pv => pv.VersionString)
                .NotEmpty()
                .WithMessage("Version string cannot be empty");
            RuleFor(pv => pv.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");
        }
    }
}
