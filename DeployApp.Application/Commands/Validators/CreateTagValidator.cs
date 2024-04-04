using DeployApp.Application.Repositories;
using FluentValidation;

namespace DeployApp.Application.Commands.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTag>
    {
        public CreateTagValidator(ITagRepository tagRepository)
        {
            RuleFor(t => t.createTagDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tag name cannot be empty")
                .MustAsync(async (name, cancellationToken) =>
                {
                    return !await tagRepository.TagWithNameAlreadyExistsAsync(name);
                }).WithMessage("Tag with same name already exists");
            RuleFor(t => t.createTagDto.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tag description cannot be empty");
        }
    }
}
