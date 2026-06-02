using MediatR;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Queries.GetFavorites;
public record GetFavoritesQuery() : IRequest<IList<VaultItemDto>>;
