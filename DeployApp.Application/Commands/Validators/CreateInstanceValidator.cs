using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateInstanceValidator : AbstractValidator<CreateInstance>
    {
        public CreateInstanceValidator()
        {
            RuleFor(i => i.dto.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(100)
                .WithMessage("Name cannot be longer then 100 characters");
            RuleFor(i => i.dto.Secret)
                .NotEmpty()
                .WithMessage("Secret cannot be empty");
            RuleFor(i => i.dto.Key)
                .NotEmpty()
                .WithMessage("Key cannot be empty");
            RuleFor(i => i.dto.TypeDescription)
                .NotEmpty()
                .WithMessage("TypeDescription cannot be empty")
                .MaximumLength(250)
                .WithMessage("TypeDescription cannot be longer then 250 characters");
        }
    }
}
