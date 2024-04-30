using DeployApp.Application.Dtos;
using DeployApp.Application.Repositories;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class UpdateProjectInstanceValidator : AbstractValidator<UpdateInstanceDto>
    {
        public UpdateProjectInstanceValidator()
        {
            RuleFor(i => i.TypeDescription)
                .NotEmpty()
                .WithMessage("Type description cannot be empty")
                .MaximumLength(250)
                .WithMessage("Type description cannot be longer then 250 characters");
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(100)
                .WithMessage("Name cannot be longer then 100 characters");
            RuleFor(i => i.Key)
                .NotEmpty()
                .WithMessage("Key cannot be empty");
            RuleFor(i => i.Secret)
                .NotEmpty()
                .WithMessage("Secret cannot be empty");
        }
    }
}
