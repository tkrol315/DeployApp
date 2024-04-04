using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateTagValidator : AbstractValidator<UpdateTag>
    {
        public UpdateTagValidator()
        {
            RuleFor(t => t.updateTagDto.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tag description cannot be empty");
        }
    }
}
