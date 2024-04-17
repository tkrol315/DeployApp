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
                .WithMessage("Title cannot be empty");
            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");
            RuleFor(r => r.IsActive)
                .NotEmpty()
                .WithMessage("IsActive cannot be empty");
            RuleFor(r => r.YtCode)
                .NotEmpty()
                .WithMessage("YtCode cannot be null");
            RuleFor(r => r.RepositoryUrl)
                .NotEmpty()
                .WithMessage("RepositoryURL cannot be empty");
        }
    }
}
