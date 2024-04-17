using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateProjectValidator : AbstractValidator<CreateProject>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.dto.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty");
            RuleFor(p => p.dto.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");
            RuleFor(p => p.dto.IsActive)
                .NotEmpty()
                .WithMessage("IsActive cannot be empty");
            RuleFor(p => p.dto.YtCode)
                .NotEmpty()
                .WithMessage("YtCode cannot be empty");
            RuleFor(p => p.dto.RepositoryUrl)
                .NotEmpty()
                .WithMessage("RepositoryURL cannot be empty");
        }
    }
}
