using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateTagValidator : AbstractValidator<UpdateTagDto>
    {
        public UpdateTagValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty()
                .WithMessage("Tag Name cannot be empty")
                .MaximumLength(100)
                .WithMessage("Tag name cannot be longer then 100 characters");
            RuleFor(t => t.Description)
                .MaximumLength(250)
                .WithMessage("Description cannot be longer then 250 characters");
        }
    }
}
