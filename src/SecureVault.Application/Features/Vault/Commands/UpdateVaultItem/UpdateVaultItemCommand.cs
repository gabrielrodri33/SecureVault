using MediatR;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Commands.UpdateVaultItem;
public record UpdateVaultItemCommand(Guid Id, string Title, string? Username, string Password, string? Url, string? Notes, Guid? CollectionId, bool IsFavorite) : IRequest<VaultItemDto>;
