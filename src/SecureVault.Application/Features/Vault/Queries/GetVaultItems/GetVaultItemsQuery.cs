using MediatR;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Queries.GetVaultItems;
public record GetVaultItemsQuery(string? Search, Guid? CollectionId, int Page = 1, int PageSize = 20) : IRequest<PaginatedResult<VaultItemDto>>;
