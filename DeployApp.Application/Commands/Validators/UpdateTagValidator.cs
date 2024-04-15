using DeployApp.Application.Dtos;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateTagValidator : AbstractValidator<UpdateTagDto>
    {
        public UpdateTagValidator()
        {
            RuleFor(t => t.Name)
                 .NotNull()
                .NotEmpty()
                .WithMessage("Tag Name cannot be empty"); 
        }
    }
}
