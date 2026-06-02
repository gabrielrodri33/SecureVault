using MediatR;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Queries.GetVaultItem;
public record GetVaultItemQuery(Guid Id) : IRequest<VaultItemDetailDto>;
