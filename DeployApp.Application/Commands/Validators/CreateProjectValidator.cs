using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateProjectValidator : AbstractValidator<CreateProject>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.dto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Title cannot be empty");
            RuleFor(p => p.dto.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Description cannot be empty");
            RuleFor(p => p.dto.IsActive)
                .NotNull()
                .NotEmpty()
                .WithMessage("IsActive cannot be empty");
            RuleFor(p => p.dto.YtCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("YtCode cannot be empty");
            RuleFor(p => p.dto.RepositoryUrl)
                .NotNull()
                .NotEmpty()
                .WithMessage("RepositoryURL cannot be empty");
        }
    }
}
