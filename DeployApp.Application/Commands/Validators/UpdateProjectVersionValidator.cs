using DeployApp.Application.Dtos;
using FluentValidation;
using System.Data;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateProjectVersionValidator : AbstractValidator<UpdateProjectVersionDto>
    {
        public UpdateProjectVersionValidator()
        {
           
            RuleFor(pv => pv.VersionString)
                .NotEmpty()
                .WithMessage("VersionString cannot be empty");
            RuleFor(pv => pv.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");
        }
    }
}
