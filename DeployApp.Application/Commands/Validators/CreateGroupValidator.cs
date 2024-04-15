using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateGroupValidator : AbstractValidator<CreateGroup>
    {
        public CreateGroupValidator()
        {
            RuleFor(g => g.dto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name cannot be empty");
        }
    }
}
