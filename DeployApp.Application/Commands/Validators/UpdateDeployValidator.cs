using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateDeployValidator : AbstractValidator<UpdateDeployDto>
    {
        public UpdateDeployValidator()
        {
            RuleFor(u => u.VersionString)
                .NotEmpty()
                .WithMessage("VersionString cannot be empty");
                 RuleFor(u => u.Start)
                .NotEmpty()
                .WithMessage("Start cannot be empty");
            RuleFor(u => u.End)
               .NotEmpty()
               .WithMessage("End cannot be empty");
            RuleFor(u => u.IsActive)
               .NotEmpty()
               .WithMessage("IsActive cannot be empty");
        }
    }
}
