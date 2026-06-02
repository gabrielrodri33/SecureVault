using FluentValidation;
namespace SecureVault.Application.Features.Vault.Commands.UpdateVaultItem;
public class UpdateVaultItemCommandValidator : AbstractValidator<UpdateVaultItemCommand>
{
    public UpdateVaultItemCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Password).NotEmpty();
    }
}
