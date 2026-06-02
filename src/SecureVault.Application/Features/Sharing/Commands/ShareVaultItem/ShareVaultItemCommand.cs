using MediatR;
namespace SecureVault.Application.Features.Sharing.Commands.ShareVaultItem;
public record ShareVaultItemCommand(Guid VaultItemId, string SharedWithUserEmail, bool CanEdit) : IRequest<Guid>;
