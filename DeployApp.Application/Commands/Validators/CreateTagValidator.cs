using DeployApp.Application.Services;
using FluentValidation;
using System.Security.Cryptography;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTag>
    {
        public CreateTagValidator(ITagReadService tagReadService)
        {
            RuleFor(t => t.createTagDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nazwa tagu nie może być pusta")
                .MustAsync(async (name, cancellationToken) =>
                {
                    return !await tagReadService.TagNameAlreadyExistsAsync(name);
                }).WithMessage("Tag o takiej nazwie już istnieje");
            RuleFor(t => t.createTagDto.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Opis tagu nie może być pusty");
        }
    }
}
