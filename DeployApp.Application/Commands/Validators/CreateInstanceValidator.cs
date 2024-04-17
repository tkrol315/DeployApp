using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateInstanceValidator : AbstractValidator<CreateInstance>
    {
        public CreateInstanceValidator()
        {
            RuleFor(i => i.dto.Secret)
                .NotEmpty()
                .WithMessage("Secret cannot be empty");
            RuleFor(i => i.dto.Key)
                .NotEmpty()
                .WithMessage("Key cannot be empty");
            RuleFor(i => i.dto.TypeDescription)
                .NotEmpty()
                .WithMessage("TypeDescription id cannot be empty");
        }
    }
}
