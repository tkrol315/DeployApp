using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateProjectValidator : AbstractValidator<CreateProject>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.dto.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty")
                .MaximumLength(100)
                .WithMessage("Title cannot be longer then 100 characters");
            RuleFor(p => p.dto.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty")
                .MaximumLength(250)
                .WithMessage("Descritpion cannot be longer then 250 characters");
            RuleFor(p => p.dto.IsActive)
                .NotEmpty()
                .WithMessage("IsActive cannot be empty");
            RuleFor(p => p.dto.YtCode)
                .NotEmpty()
                .WithMessage("YtCode cannot be empty")
                .MaximumLength(10)
                .WithMessage("YtCode cannot be longer then 10 characters");
            RuleFor(p => p.dto.RepositoryUrl)
                .NotEmpty()
                .WithMessage("RepositoryURL cannot be empty")
                .MaximumLength(250)
                .WithMessage("RepositorURL cannot be longer then 250 characters");
        }
    }
}
