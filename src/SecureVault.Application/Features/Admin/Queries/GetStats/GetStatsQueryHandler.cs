using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Admin.DTOs;
namespace SecureVault.Application.Features.Admin.Queries.GetStats;
public class GetStatsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetStatsQuery, StatsDto>
{
    public async Task<StatsDto> Handle(GetStatsQuery request, CancellationToken cancellationToken)
    {
        var users = await db.Users.CountAsync(cancellationToken);
        var items = await db.VaultItems.CountAsync(cancellationToken);
        var collections = await db.Collections.CountAsync(cancellationToken);
        return new StatsDto(users, items, collections);
    }
}
