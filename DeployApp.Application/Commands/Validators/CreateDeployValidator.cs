using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateDeployValidator : AbstractValidator<CreateDeployDto>
    {
        public CreateDeployValidator()
        {
            RuleFor(d => d.Start)
                .NotEmpty()
                .WithMessage("Start cannot be empty");
            RuleFor(d => d.End)
                .NotEmpty()
                .WithMessage("End cannot be empty");
            RuleFor(d => d.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");
            RuleFor(d => d.IsActive)
                .NotEmpty()
                .WithMessage("IsActive cannot be empty");
        }
    }
}
