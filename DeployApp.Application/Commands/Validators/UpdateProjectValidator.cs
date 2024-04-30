using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty")
                .MaximumLength(100)
                .WithMessage("Title cannot be longer then 100 characters");
            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty")
                .MaximumLength(250)
                .WithMessage("Description cannot be longer then 250 characters");
            RuleFor(r => r.IsActive)
                .NotEmpty()
                .WithMessage("IsActive cannot be empty");
            RuleFor(r => r.YtCode)
                .NotEmpty()
                .WithMessage("YtCode cannot be null")
                .MaximumLength(10)
                .WithMessage("YtCode cannot be longer then 10 characters");
            RuleFor(r => r.RepositoryUrl)
                .NotEmpty()
                .WithMessage("RepositoryURL cannot be empty")
                .MaximumLength(250)
                .WithMessage("RepositoryURL cannot be longer then 250 characters");
        }
    }
}
