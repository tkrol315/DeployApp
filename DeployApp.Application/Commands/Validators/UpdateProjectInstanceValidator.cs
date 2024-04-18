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
                .WithMessage("Type description cannot be empty");
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");
            RuleFor(i => i.Key)
                .NotEmpty()
                .WithMessage("Key cannot be empty");
            RuleFor(i => i.Secret)
                .NotEmpty()
                .WithMessage("Secret cannot be empty");
        }
    }
}
