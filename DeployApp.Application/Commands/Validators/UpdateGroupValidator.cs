using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupDto>
    {
        public UpdateGroupValidator()
        {
            RuleFor(g => g.Name)
               .NotEmpty()
               .WithMessage("Group name cannot be empty")
               .MaximumLength(100)
               .WithMessage("Group name cannot be longer then 100 characters");
            RuleFor(g => g.Description)
                .MaximumLength(250)
                .WithMessage("Group description cannot be longer then 250 characters");
        }
    }
}
