using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProject>
    {
        public UpdateProjectValidator()
        {
            RuleFor(r => r.dto.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Title cannot be empty");
            RuleFor(r => r.dto.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description cannot be empty");
            RuleFor(r => r.dto.IsActive)
                .NotEmpty()
                .NotNull()
                .WithMessage("IsActive cannot be empty");
            RuleFor(r => r.dto.YtCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("YtCode cannot be null");
            RuleFor(r => r.dto.RepositoryUrl)
                .NotEmpty()
                .NotNull()
                .WithMessage("RepositoryURL cannot be empty");
        }
    }
}
