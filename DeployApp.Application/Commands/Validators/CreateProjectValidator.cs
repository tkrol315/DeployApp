using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title cannot be empty")
                .MaximumLength(100).WithMessage("Title cannot be longer then 100 characters");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .MaximumLength(250).WithMessage("Descritpion cannot be longer then 250 characters");
             RuleFor(p => p.IsActive)
                .NotNull()
                .Must(value => value == true || value == false).WithMessage("IsActive must be true or false");
            RuleFor(p => p.YtCode)
                .NotEmpty().WithMessage("YtCode cannot be empty")
                .MaximumLength(10).WithMessage("YtCode cannot be longer then 10 characters");
            RuleFor(p => p.RepositoryUrl)
                .NotEmpty().WithMessage("RepositoryURL cannot be empty")
                .MaximumLength(250).WithMessage("RepositorURL cannot be longer then 250 characters");
        }
    }
}
