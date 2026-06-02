using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Queries.GetFavorites;
public class GetFavoritesQueryHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<GetFavoritesQuery, IList<VaultItemDto>>
{
    public async Task<IList<VaultItemDto>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        return await db.VaultItems.Where(v => v.UserId == currentUser.UserId && v.IsFavorite)
            .OrderBy(v => v.Title)
            .Select(v => new VaultItemDto(v.Id, v.Title, v.Username, v.Url, v.IsFavorite, v.CollectionId, v.CreatedAt, v.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}
