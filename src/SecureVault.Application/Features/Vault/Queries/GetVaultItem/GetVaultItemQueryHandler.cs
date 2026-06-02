using MediatR;
using Microsoft.EntityFrameworkCore;
using SecureVault.Application.Common.Exceptions;
using SecureVault.Application.Common.Interfaces;
using SecureVault.Application.Features.Vault.DTOs;
namespace SecureVault.Application.Features.Vault.Queries.GetVaultItem;
public class GetVaultItemQueryHandler(IApplicationDbContext db, ICurrentUserService currentUser, ICryptoService crypto) : IRequestHandler<GetVaultItemQuery, VaultItemDetailDto>
{
    public async Task<VaultItemDetailDto> Handle(GetVaultItemQuery request, CancellationToken cancellationToken)
    {
        var item = await db.VaultItems.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.VaultItem), request.Id);
        var hasAccess = item.UserId == currentUser.UserId || await db.SharedItems.AnyAsync(s => s.VaultItemId == request.Id && s.SharedWithUserId == currentUser.UserId, cancellationToken);
        if (!hasAccess) throw new ForbiddenException();
        return new VaultItemDetailDto(item.Id, item.Title, item.Username, item.Url, item.IsFavorite, item.CollectionId, crypto.Decrypt(item.EncryptedPassword), item.Notes, item.CreatedAt, item.UpdatedAt);
    }
}
