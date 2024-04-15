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
                .NotNull()
                .WithMessage("Title cannot be empty");
            RuleFor(r => r.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description cannot be empty");
            RuleFor(r => r.IsActive)
                .NotEmpty()
                .NotNull()
                .WithMessage("IsActive cannot be empty");
            RuleFor(r => r.YtCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("YtCode cannot be null");
            RuleFor(r => r.RepositoryUrl)
                .NotEmpty()
                .NotNull()
                .WithMessage("RepositoryURL cannot be empty");
        }
    }
}
