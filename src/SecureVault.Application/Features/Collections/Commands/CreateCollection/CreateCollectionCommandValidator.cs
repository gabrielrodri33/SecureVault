using FluentValidation;
namespace SecureVault.Application.Features.Collections.Commands.CreateCollection;
public class CreateCollectionCommandValidator : AbstractValidator<CreateCollectionCommand>
{
    public CreateCollectionCommandValidator() { RuleFor(x => x.Name).NotEmpty().MaximumLength(100); }
}
