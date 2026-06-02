using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Collections.DTOs;
namespace SecureVault.Application.Features.Collections.Queries.GetCollections;
public class GetCollectionsQueryHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<GetCollectionsQuery, IList<CollectionDto>>
{
    public async Task<IList<CollectionDto>> Handle(GetCollectionsQuery request, CancellationToken cancellationToken)
    {
        return await db.Collections.Where(c => c.UserId == currentUser.UserId).Include(c => c.Items)
            .OrderBy(c => c.Name)
            .Select(c => new CollectionDto(c.Id, c.Name, c.Description, c.Items.Count, c.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
