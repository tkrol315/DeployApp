using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class AssignGroupValidator : AbstractValidator<AssignGroupDto>
    {
        public AssignGroupValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("GroupName cannot be empty");
        }
    }
}
