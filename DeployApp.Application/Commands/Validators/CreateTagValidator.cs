using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTag>
    {
        public CreateTagValidator()
        {
            RuleFor(t => t.createTagDto.Name)
                .NotEmpty()
                .WithMessage("Tag name cannot be empty")
                .MaximumLength(100)
                .WithMessage("Tag name cannot be longer then 100 characters");

            RuleFor(t => t.createTagDto.Description)
                .MaximumLength(250)
                .WithMessage("Tag description cannot be longer then 250 characters");
        }
    }
}
