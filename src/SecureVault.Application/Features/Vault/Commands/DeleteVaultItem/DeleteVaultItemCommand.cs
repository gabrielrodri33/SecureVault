using MediatR;
namespace SecureVault.Application.Features.Vault.Commands.DeleteVaultItem;
public record DeleteVaultItemCommand(Guid Id) : IRequest<Unit>;
