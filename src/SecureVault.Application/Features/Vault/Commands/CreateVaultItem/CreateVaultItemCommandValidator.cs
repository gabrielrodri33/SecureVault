using FluentValidation;
namespace SecureVault.Application.Features.Vault.Commands.CreateVaultItem;
public class CreateVaultItemCommandValidator : AbstractValidator<CreateVaultItemCommand>
{
    public CreateVaultItemCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Password).NotEmpty();
    }
}
