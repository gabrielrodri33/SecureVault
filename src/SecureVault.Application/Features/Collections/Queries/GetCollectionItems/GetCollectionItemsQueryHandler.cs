using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Collections.Queries.GetCollectionItems;
public class GetCollectionItemsQueryHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<GetCollectionItemsQuery, IList<VaultItemDto>>
{
    public async Task<IList<VaultItemDto>> Handle(GetCollectionItemsQuery request, CancellationToken cancellationToken)
    {
        var col = await db.Collections.FirstOrDefaultAsync(c => c.Id == request.CollectionId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Collection), request.CollectionId);
        if (col.UserId != currentUser.UserId) throw new ForbiddenException();
        return await db.VaultItems.Where(v => v.CollectionId == request.CollectionId)
            .Select(v => new VaultItemDto(v.Id, v.Title, v.Username, v.Url, v.IsFavorite, v.CollectionId, v.CreatedAt, v.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}
