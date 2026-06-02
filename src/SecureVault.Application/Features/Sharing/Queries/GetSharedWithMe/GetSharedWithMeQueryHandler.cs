using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Auth.DTOs;
using SecureVault.Application.Features.Sharing.DTOs;
namespace SecureVault.Application.Features.Sharing.Queries.GetSharedWithMe;
public class GetSharedWithMeQueryHandler(IApplicationDbContext db, ICurrentUserService currentUser) : IRequestHandler<GetSharedWithMeQuery, IList<SharedVaultItemDto>>
{
    public async Task<IList<SharedVaultItemDto>> Handle(GetSharedWithMeQuery request, CancellationToken cancellationToken)
    {
        return await db.SharedItems
            .Where(s => s.SharedWithUserId == currentUser.UserId)
            .Include(s => s.VaultItem).ThenInclude(v => v.User)
            .Select(s => new SharedVaultItemDto(s.Id, s.VaultItemId, s.VaultItem.Title, s.VaultItem.Username, s.VaultItem.Url, s.VaultItem.IsFavorite,
                new UserDto(s.VaultItem.User.Id, s.VaultItem.User.Email, s.VaultItem.User.Name, s.VaultItem.User.Role.ToString()), s.CanEdit, s.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}
