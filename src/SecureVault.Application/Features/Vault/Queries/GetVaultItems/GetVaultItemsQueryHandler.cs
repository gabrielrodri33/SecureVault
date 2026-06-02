using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Queries.GetVaultItems;
public class GetVaultItemsQueryHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<GetVaultItemsQuery, PaginatedResult<VaultItemDto>>
{
    public async Task<PaginatedResult<VaultItemDto>> Handle(GetVaultItemsQuery request, CancellationToken cancellationToken)
    {
        var query = db.VaultItems.Where(v => v.UserId == currentUser.UserId);
        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(v => v.Title.Contains(request.Search) || (v.Username != null && v.Username.Contains(request.Search)) || (v.Url != null && v.Url.Contains(request.Search)));
        if (request.CollectionId.HasValue)
            query = query.Where(v => v.CollectionId == request.CollectionId);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.OrderByDescending(v => v.UpdatedAt).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize)
            .Select(v => new VaultItemDto(v.Id, v.Title, v.Username, v.Url, v.IsFavorite, v.CollectionId, v.CreatedAt, v.UpdatedAt))
            .ToListAsync(cancellationToken);
        return new PaginatedResult<VaultItemDto>(items, total, request.Page, request.PageSize);
    }
}
