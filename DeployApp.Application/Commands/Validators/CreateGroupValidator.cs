using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateGroupValidator : AbstractValidator<CreateGroup>
    {
        public CreateGroupValidator()
        {
            RuleFor(g => g.dto.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(100)
                .WithMessage("Name cannot be longer then 100 characters");
            RuleFor(g => g.dto.Description)
                .MaximumLength(250)
                .WithMessage("Description cannot be longer then 250 characters");
        }
    }
}
