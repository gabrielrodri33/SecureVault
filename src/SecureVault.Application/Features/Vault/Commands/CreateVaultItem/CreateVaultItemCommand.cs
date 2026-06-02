using MediatR;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Commands.CreateVaultItem;
public record CreateVaultItemCommand(string Title, string? Username, string Password, string? Url, string? Notes, Guid? CollectionId, bool IsFavorite = false) : IRequest<VaultItemDto>;
